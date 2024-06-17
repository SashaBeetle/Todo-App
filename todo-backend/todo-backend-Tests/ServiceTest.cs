using Microsoft.Extensions.DependencyInjection;
using todo_backend.Infrastructure.Interfaces;
using todo_backend.Infrastructure.Services;
using todo_backend.Domain.Models;
using Moq;

namespace todo_backend_Tests
{
    [TestClass]
    public class ServiceTest
    {
        [TestMethod]
        public void Test_DI_Registration()
        {
            var services = new ServiceCollection();

            var mockCardService = new Mock<IDbEntityService<Card>>();
            var mockBoardService = new Mock<IDbEntityService<Board>>();
            var mockCatalogService = new Mock<IDbEntityService<Catalog>>();


            services.AddScoped<IDbEntityService<Card>>(_ => mockCardService.Object);
            services.AddTransient<IDbEntityService<Board>>(_ => mockBoardService.Object);
            services.AddSingleton<IDbEntityService<Catalog>>(_ => mockCatalogService.Object);


            var serviceProvider = services.BuildServiceProvider();

            var dataCardService = serviceProvider.GetService<IDbEntityService<Card>>();
            var dataBoardService = serviceProvider.GetService<IDbEntityService<Board>>();
            var dataCatalogService = serviceProvider.GetService<IDbEntityService<Catalog>>();

            
            Assert.IsNotNull(dataCardService);
            Assert.IsInstanceOfType<IDbEntityService<Card>>(dataCardService);
            Assert.IsNotNull(dataBoardService);
            Assert.IsInstanceOfType<IDbEntityService<Board>>(dataBoardService);
            Assert.IsNotNull(dataCatalogService);
            Assert.IsInstanceOfType<IDbEntityService<Catalog>>(dataCatalogService);
        }

        [TestMethod]
        public void Test_Singleton_Service_Registration()
        {
            // Arrange
            var services = new ServiceCollection();
            var mockCardService = new Mock<IDbEntityService<Card>>();
            services.AddSingleton<IDbEntityService<Card>>(_ => mockCardService.Object);

            // Act
            var serviceProvider = services.BuildServiceProvider();

            // Assert
            var singletonService1 = serviceProvider.GetService<IDbEntityService<Card>>();
            var singletonService2 = serviceProvider.GetService<IDbEntityService<Card>>();

            Assert.IsNotNull(singletonService1);
            Assert.AreSame(singletonService1, singletonService2);
        }

        [TestMethod]
        public void Test_Scoped_Service_Registration()
        {
            // Arrange
            var services = new ServiceCollection();
            var mockCatalogService = new Mock<IDbEntityService<Catalog>>();

            services.AddScoped<IDbEntityService<Catalog>>(_ => mockCatalogService.Object);

            // Act
            var serviceProvider = services.BuildServiceProvider();

            // Assert
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedService1 = scope.ServiceProvider.GetService<IDbEntityService<Catalog>>();
                var scopedService2 = scope.ServiceProvider.GetService<IDbEntityService<Catalog>>();

                Assert.IsNotNull(scopedService1);
                Assert.AreSame(scopedService1, scopedService2);
            }
        }

        [TestMethod]
        public void Test_Transient_Service_Registration()
        {
            // Arrange
            var services = new ServiceCollection();
            var mockBoardService = new Mock<IDbEntityService<Board>>();
            var mockCardService = new Mock<IDbEntityService<Card>>();


            services.AddTransient<IDbEntityService<Board>>(_ => mockBoardService.Object);
            // Act
            var serviceProvider = services.BuildServiceProvider();

            // Assert

            using (var scope = serviceProvider.CreateScope())
            {
                var transientService1 = serviceProvider.GetService<IDbEntityService<Board>>();
                var transientService2 = serviceProvider.GetService<IDbEntityService<Board>>();

                Assert.IsNotNull(transientService1);
                Assert.IsNotNull(transientService2);

                Assert.AreSame(transientService1, transientService2);
            }
            
        }
        [TestMethod]
        public void Test_CardService()
        {
            var mockCardService = new Mock<IDbEntityService<Card>>();
            var card = new Card()
            {
                Id = 1,
                Title = "Test Card",
                Description = "Test Description",
                DueDate = new DateTime(2021, 12, 31)  
            };

            mockCardService.Setup(x => x.Create(card)).ReturnsAsync(card);

            var cardService = mockCardService.Object;

            var createdCard = cardService.Create(card).Result;

            Assert.AreEqual(card.Id, createdCard.Id);
            Assert.AreEqual(card.Title, createdCard.Title);
            Assert.AreEqual(card.Description, createdCard.Description);
            Assert.AreEqual(card.DueDate, createdCard.DueDate);
        }
        [TestMethod]
        public void Test_MoveCardService()
        {
            var mockCatalogService = new Mock<IDbEntityService<Catalog>>();
            var mockCardService = new Mock<IDbEntityService<Card>>();
            var mockHistoryItemService = new Mock<IDbEntityService<HistoryItem>>();

            var card = Mock.Of<Card>(c =>
            c.Id == 1 && 
            c.Title == "Test Card" && 
            c.Description == "Test Description" && 
            c.DueDate == new DateTime(2021, 12, 31));

            var catalog1 = Mock.Of<Catalog>(c => 
            c.Id == 1 && 
            c.Title == "Catalog 1" && 
            c.CardsId == new List<int> { 1 });

            var catalog2 = Mock.Of<Catalog>(c => 
            c.Id == 2 && 
            c.Title == "Catalog 2" &&
            c.CardsId == new List<int>());

            mockCatalogService.Setup(x => x.GetById(1)).ReturnsAsync(catalog1);
            mockCatalogService.Setup(x => x.GetById(2)).ReturnsAsync(catalog2);
            mockCardService.Setup(x => x.GetById(1)).ReturnsAsync(card);

            var moveCardService = new MoveCardService(mockCatalogService.Object, mockCardService.Object, mockHistoryItemService.Object);

            moveCardService.MoveCard(1, 1, 2, 1).Wait();

            Assert.AreEqual(0, catalog1.CardsId.Count);
            Assert.AreEqual(1, catalog2.CardsId.Count);
            Assert.AreEqual(1, catalog2.CardsId[0]);
        }
        [TestMethod]
        public void Test_BoardService()
        {
            var mockBoardService = new Mock<IDbEntityService<Board>>();

            var board = new Board()
            {
                Id = 1,
                Title = "Test Board"
            };

            mockBoardService.Setup(x => x.Create(board)).ReturnsAsync(board);

            var boardService = mockBoardService.Object;

            var createdBoard = boardService.Create(board).Result;

            Assert.AreEqual(board.Id, createdBoard.Id);
            Assert.AreEqual(board.Title, createdBoard.Title);
        }
        [TestMethod]
        public void Test_CatalogService()
        {
            var mockCatalogService = new Mock<IDbEntityService<Catalog>>();

            var catalog = new Catalog()
            {
                Id = 1,
                Title = "Test Catalog",
                CardsId = new List<int> { 1 }
            };

            mockCatalogService.Setup(x => x.Create(catalog)).ReturnsAsync(catalog);

            var catalogService = mockCatalogService.Object;

            var createdCatalog = catalogService.Create(catalog).Result;

            Assert.AreEqual(catalog.Id, createdCatalog.Id);
            Assert.AreEqual(catalog.Title, createdCatalog.Title);
            Assert.AreEqual(catalog.CardsId.Count, createdCatalog.CardsId.Count);
        }
        [TestMethod]
        public void Test_HistoryItemService()
        {
            var mockHistoryItemService = new Mock<IDbEntityService<HistoryItem>>();

            var historyItem = new HistoryItem()
            {
                Id = 1,
                EventDescription = "Test Event",
                BoardId = 1
            };

            mockHistoryItemService.Setup(x => x.Create(historyItem)).ReturnsAsync(historyItem);

            var historyItemService = mockHistoryItemService.Object;

            var createdHistoryItem = historyItemService.Create(historyItem).Result;

            Assert.AreEqual(historyItem.Id, createdHistoryItem.Id);
            Assert.AreEqual(historyItem.EventDescription, createdHistoryItem.EventDescription);
            Assert.AreEqual(historyItem.BoardId, createdHistoryItem.BoardId);
        }

        [TestMethod]
        public void Test_Board_Entity()
        {
            var mockBoardService = new Mock<IDbEntityService<Board>>();

            var board = new Board()
            {
                Id = 1,
                Title = "Test Board",
                CatalogsId = new List<int>(),
            };

            mockBoardService.Setup(x => x.Create(board)).ReturnsAsync(board);
            mockBoardService.Setup(x => x.GetById(1)).ReturnsAsync(board);

            var boardService = mockBoardService.Object;

            var createdBoard = boardService.Create(board).Result;
            var boardById = boardService.GetById(1).Result;

            Assert.AreEqual(board.Id, createdBoard.Id);
            Assert.AreEqual(board.Title, createdBoard.Title);
        }
        [TestMethod]
        public void Test_Catalog_Entity()
        {
            var mockCatalogService = new Mock<IDbEntityService<Catalog>>();

            var catalog = new Catalog()
            {
                Id = 1,
                Title = "Test Catalog",
                CardsId = new List<int>(),
            };

            mockCatalogService.Setup(x => x.Create(catalog)).ReturnsAsync(catalog);
            mockCatalogService.Setup(x => x.GetById(1)).ReturnsAsync(catalog);
        }
        [TestMethod]
        public void Test_Card_Entity()
        {
            var mockCardService = new Mock<IDbEntityService<Card>>();

            var card = new Card()
            {
                Id = 1,
                Title = "Test Card",
                Description = "Test Description",
            };

            mockCardService.Setup(x => x.Create(card)).ReturnsAsync(card);
            mockCardService.Setup(x => x.GetById(1)).ReturnsAsync(card);
        }
        [TestMethod]
        public void Test_HistoryItem_Entity()
        {
            var mockHistoryItemService = new Mock<IDbEntityService<HistoryItem>>();

            var historyItem = new HistoryItem()
            {
                Id = 1,
                EventDescription = "Test Event",
                BoardId = 1,
            };

            mockHistoryItemService.Setup(x => x.Create(historyItem)).ReturnsAsync(historyItem);
            mockHistoryItemService.Setup(x => x.GetById(1)).ReturnsAsync(historyItem);
        }

    }
}
