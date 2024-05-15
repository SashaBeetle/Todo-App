using Microsoft.Extensions.DependencyInjection;
using todo_backend.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            services.AddScoped<IDbEntityService<Card>>(_ => mockCardService.Object);

            var serviceProvider = services.BuildServiceProvider();

            var dataService = serviceProvider.GetService<IDbEntityService<Card>>();
            Assert.IsNotNull(dataService);
            Assert.IsInstanceOfType<IDbEntityService<Card>>(dataService);
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

    }
}
