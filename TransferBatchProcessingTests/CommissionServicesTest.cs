using TransferBatchProcecingModels.Domain;
using TransferBatchProcessingData.Repositories.Interfaces;
using Moq;
using TransferBatchProcessingServices.Mappers.Interfaces;
using TransferBatchProcecingModels.DTOs;
using TransferBatchProcessingServices.Services;



namespace TransferBatchProcessingTests
{
    public class CommissionServicesTest
    {
        private readonly Mock<IRepository> _repositoryMock;
        private readonly Mock<IMapperDBtoDTO> _mapperMock;
        private readonly CommissionsService _service;


        public CommissionServicesTest()
        {
            _mapperMock = new Mock<IMapperDBtoDTO>();
            _repositoryMock = new Mock<IRepository>();
            _service = new CommissionsService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async void CalculateCommissions_ShouldReturnCorrectResults()
        {
            // Arrange
            var filePath = "test.csv";
            var transfers = new List<Transfer>
            {
                new Transfer { TransferId = "1", AccountId = "Account1", TotalTransferAmount = 1000m },
                new Transfer { TransferId = "2", AccountId = "Account1", TotalTransferAmount = 2000m },
                new Transfer { TransferId = "3", AccountId = "Account2", TotalTransferAmount = 3000m },
                new Transfer { TransferId = "4", AccountId = "Account2", TotalTransferAmount = 2000m },
            };


            var expectedCommissions = new List<TransferCommissionsDto>
            {
                new TransferCommissionsDto { AccountId = "Account1", Commission = "300" }, 
                new TransferCommissionsDto { AccountId = "Account2", Commission = "200" },   
            };

            _repositoryMock.Setup(r => r.GetTransfersAsync(filePath)).ReturnsAsync(transfers);
            _mapperMock.Setup(m => m.MapCommissions("Account1", 300m)).Returns(expectedCommissions[0]);
            _mapperMock.Setup(m => m.MapCommissions("Account2", 200m)).Returns(expectedCommissions[1]);

            // Act
            var result = await _service.CalculateCommissionsAsync(filePath);

            // Assert
            Assert.Equal(expectedCommissions[0].Commission, result.First(r => r.AccountId == "Account1").Commission);
            Assert.Equal(expectedCommissions[1].Commission, result.First(r => r.AccountId == "Account2").Commission);
        }

        [Fact]
        public async void CalculateCommissions_ShouldReturnCorrectResultsOneTransferByAccount()
        {
            // Arrange
            var filePath = "test.csv";
            var transfers = new List<Transfer>
            {
                new Transfer { TransferId = "1", AccountId = "Account1", TotalTransferAmount = 1000m },
                new Transfer { TransferId = "2", AccountId = "Account2", TotalTransferAmount = 2000m },
             
            };


            var expectedCommissions = new List<TransferCommissionsDto>
            {
                new TransferCommissionsDto { AccountId = "Account1", Commission = "100" },
                new TransferCommissionsDto { AccountId = "Account2", Commission = "0" },
            };

            _repositoryMock.Setup(r => r.GetTransfersAsync(filePath)).ReturnsAsync(transfers);
            _mapperMock.Setup(m => m.MapCommissions("Account1", 100m)).Returns(expectedCommissions[0]);
            _mapperMock.Setup(m => m.MapCommissions("Account2", 0m)).Returns(expectedCommissions[1]);

            // Act
            var result = await _service.CalculateCommissionsAsync(filePath);

            // Assert
            Assert.Equal(expectedCommissions[0].Commission, result.First(r => r.AccountId == "Account1").Commission);
            Assert.Equal(expectedCommissions[1].Commission, result.First(r => r.AccountId == "Account2").Commission);
        }

        [Fact]
        public async Task CalculateCommissions_ShouldHandleSingleAccountCommissions()
        {
            // Arrange
            var filePath = "test.txt";
            var transfers = new List<Transfer>
        {
            new Transfer { TransferId = "1", AccountId = "Account1", TotalTransferAmount = 1000m },
            new Transfer { TransferId = "2", AccountId = "Account1", TotalTransferAmount = 1500m },
        };

            var expectedCommissions = new List<TransferCommissionsDto>
            {
                new TransferCommissionsDto { AccountId = "Account1", Commission = "100" }, 
            };

            _repositoryMock.Setup(r => r.GetTransfersAsync(filePath)).ReturnsAsync(transfers);
            _mapperMock.Setup(m => m.MapCommissions("Account1", 100m)).Returns(expectedCommissions[0]);

            // Act
            var result = await _service.CalculateCommissionsAsync(filePath);

            // Assert
            Assert.Single(result);
            Assert.Equal(expectedCommissions[0].Commission, result.First().Commission);
        }
    }
}
