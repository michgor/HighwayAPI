using System;
using System.Collections.Generic;
using FluentAssertions;
using Highway.DAL.DataSource;
using Highway.DAL.DomainModels.Routes;
using Highway.DAL.DomainModels.TollStations;
using Highway.DAL.DomainModels.Vehicles;
using Highway.DAL.Helpers;
using Highway.DomainModels.TollStations;
using Highway.Repositories.TollStations;
using Moq;
using NUnit.Framework;

namespace Highway.Tests.Repositories
{
    [TestFixture]
    public class TollStationRepositoryTests
    {
        private Mock<IStubDataRepository> mockStubDataRepository;
        private Mock<IDateTimeProvider> mockDateTimeProvider;
        private TollStationRepository sut;

        [OneTimeSetUp]
        public void Setup()
        {
            this.mockStubDataRepository = new Mock<IStubDataRepository>();
            this.mockDateTimeProvider = new Mock<IDateTimeProvider>();
            this.sut = new TollStationRepository(this.mockStubDataRepository.Object, this.mockDateTimeProvider.Object);
        }

        [Test]
        public void GetGateAccessTypes_WhenTollStationIsNotInDatabase_ThenReturnsFailedResult()
        {
            // Arrange
            this.mockStubDataRepository.Setup(x => x.TollStations).Returns(new List<TollStation>());

            // Act
            var result = this.sut.GetGateAccessTypes("station-not-in-db", "gate-not-in-db");

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Data.Should().BeNull();
        }

        [Test]
        public void GetGateAccessTypes_WhenTollStationIsInDatabase_ThenReturnsGateAccessOptions()
        {
            // Arrange
            var gateAccessTypes = new List<GateAccessType> { GateAccessType.Ticket };
            var gatePublicId = "wro-g1";
            var tollStationPublicId = "wro-t1";
            var gate = new Gate("test-gate", gateAccessTypes, new List<GatePaymentOption>(), gatePublicId);
            var tollStation = new TollStation("test-toll-station", new List<Gate> { gate }, null, tollStationPublicId);
            this.mockStubDataRepository.Setup(x => x.TollStations).Returns(new List<TollStation> { tollStation });

            // Act
            var result = this.sut.GetGateAccessTypes(tollStationPublicId, gatePublicId);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().BeEquivalentTo(gateAccessTypes);
        }

        [TestCase("Motorcycle")]
        [TestCase("Car")]
        [TestCase("Truck")]
        [TestCase("LongVehicle")]
        public void GetPriceForRoute_WhenVehicleHasToPayOnEntryGate_ThenReturnsCorrectPrice(string vehicleCategoryName)
        {
            // Arrange
            var gatePublicId = "wro-g1";
            var gateAccessTypes = new List<GateAccessType> { GateAccessType.Payment };
            var entryGate = new Gate("test-gate", gateAccessTypes, new List<GatePaymentOption>(), gatePublicId);

            var vehicleCategory = Enum.Parse<VehicleCategory>(vehicleCategoryName);
            var vehicle = new Vehicle(vehicleCategory);

            var validFrom = new DateTime(2019, 5, 14);
            var validTo = new DateTime(2019, 5, 16);
            var price = 100;
            var routePrice = new RoutePrice(price, validFrom, validTo, new Route(entryGate), vehicle);

            this.mockStubDataRepository.Setup(x => x.RoutePrices).Returns(new List<RoutePrice> { routePrice });
            this.mockDateTimeProvider.Setup(x => x.Now).Returns(new DateTime(2019, 5, 15));

            // Act
            var result = this.sut.GetPriceForRoute(gatePublicId, string.Empty, vehicleCategory);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().Be(price);
        }

        [Test]
        public void GetPriceForRoute_WhenVehicleHasToPayOnEntryGateButHasInactivePrice_ThenReturnsFailedResult()
        {
            // Arrange
            var gatePublicId = "wro-g1";
            var gateAccessTypes = new List<GateAccessType> { GateAccessType.Payment };
            var entryGate = new Gate("test-gate", gateAccessTypes, new List<GatePaymentOption>(), gatePublicId);

            var validFrom = new DateTime(2019, 5, 14);
            var validTo = new DateTime(2019, 5, 16);
            var price = 100;
            var routePrice = new RoutePrice(price, validFrom, validTo, new Route(entryGate), new Vehicle(VehicleCategory.Car));
            var date = new DateTime(2019, 5, 20);

            this.mockStubDataRepository.Setup(x => x.RoutePrices).Returns(new List<RoutePrice> { routePrice });
            this.mockDateTimeProvider.Setup(x => x.Now).Returns(date);

            // Act
            var result = this.sut.GetPriceForRoute(gatePublicId, string.Empty, VehicleCategory.Car);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Data.Should().Be(default(decimal));
        }

        [TestCase("Motorcycle", "Ticket", "Payment")]
        [TestCase("Motorcycle", "ViaToll", "Payment")]
        [TestCase("Car", "Ticket", "Payment")]
        [TestCase("Car", "ViaToll", "Payment")]
        [TestCase("Truck", "Ticket", "Payment")]
        [TestCase("Truck", "ViaToll", "Payment")]
        [TestCase("LongVehicle", "Ticket", "Payment")]
        [TestCase("LongVehicle", "ViaToll", "Payment")]
        public void GetPriceForRoute_WhenRouteHasActivePrice_ThenReturnsCorrectPrice(string vehicleCategoryName, string entryAccess, string exitAccess)
        {
            // Arrange
            var entryGatePublicId = "wro-g1";
            var entryGateAccessTypes = new List<GateAccessType> { Enum.Parse<GateAccessType>(entryAccess) };
            var entryGate = new Gate("test-gate", entryGateAccessTypes, new List<GatePaymentOption>(), entryGatePublicId);

            var exitGatePublicId = "wro-g2";
            var exitGateAccessTypes = new List<GateAccessType> { Enum.Parse<GateAccessType>(exitAccess) };
            var exitGate = new Gate("test-gate", exitGateAccessTypes, new List<GatePaymentOption>(), exitGatePublicId);

            var vehicleCategory = Enum.Parse<VehicleCategory>(vehicleCategoryName);
            var vehicle = new Vehicle(vehicleCategory);

            var validFrom = new DateTime(2019, 5, 14);
            var validTo = new DateTime(2019, 5, 16);
            var price = 100;
            var routePrice = new RoutePrice(price, validFrom, validTo, new Route(entryGate, exitGate), vehicle);
            var date = new DateTime(2019, 5, 15);

            this.mockStubDataRepository.Setup(x => x.RoutePrices).Returns(new List<RoutePrice> { routePrice });
            this.mockDateTimeProvider.Setup(x => x.Now).Returns(date);

            // Act
            var result = this.sut.GetPriceForRoute(entryGatePublicId, exitGatePublicId, vehicleCategory);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().Be(price);
        }

        [Test]
        public void GetPriceForRoute_WhenVehicleEntersGateBeforePriceChanges_ThenOnExitGatePriceIsUnchanged()
        {
            // Arrange
            var entryGatePublicId = "wro-g1";
            var entryGateAccessTypes = new List<GateAccessType> { GateAccessType.Ticket };
            var entryGate = new Gate("test-gate", entryGateAccessTypes, new List<GatePaymentOption>(), entryGatePublicId);

            var r1ValidFrom = new DateTime(2019, 5, 16, 12, 0, 0);
            var r1ValidTo = new DateTime(2019, 5, 16, 13, 0, 0);
            var r1Price = 100;
            var routePrice1 = new RoutePrice(r1Price, r1ValidFrom, r1ValidTo, new Route(entryGate), new Vehicle(VehicleCategory.Car));

            var r2ValidFrom = new DateTime(2019, 5, 16, 12, 50, 0);
            var r2ValidTo = new DateTime(2019, 5, 27, 12, 0, 0);
            var r2Price = 200;
            var routePrice2 = new RoutePrice(r2Price, r2ValidFrom, r2ValidTo, new Route(entryGate), new Vehicle(VehicleCategory.Car));

            var date = new DateTime(2019, 5, 16, 12, 30, 0);

            this.mockStubDataRepository.Setup(x => x.RoutePrices).Returns(new List<RoutePrice> { routePrice1, routePrice2 });
            this.mockDateTimeProvider.Setup(x => x.Now).Returns(date);

            // Act
            var result = this.sut.GetPriceForRoute(entryGatePublicId, string.Empty, VehicleCategory.Car);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().Be(r1Price);
        }
    }
}