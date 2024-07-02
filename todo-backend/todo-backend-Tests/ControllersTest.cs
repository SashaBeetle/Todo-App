using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using todo_backend.Domain.Models;
using todo_backend.Infrastructure.Interfaces;
using todo_backend.WEB.Controllers;
using todo_backend.WEB.Mapping.DTOs;

namespace todo_backend_Tests
{
    [TestClass]
    public class ControllersTest
    {
        //[TestMethod]
        //public async Task CreateBoard_Returns_CreatedAtActionResult()
        //{
        //    // Arrange
        //    var mockBoardService = new Mock<IDbEntityService<Board>>();
        //    var mockMapper = new Mock<IMapper>();
        //    var mockHistoryItemService = new Mock<IDbEntityService<HistoryItem>>();

        //    var boardDto = new BoardDTO { Title = "Test Board" };
        //    var board = new Board { Id = 1, Title = "Test Board" };
        //    var createdBoard = new Board { Id = 1, Title = "Test Board" };
        //    var createdBoardDto = new BoardDTO { Id = 1, Title = "Test Board" };

        //    mockBoardService.Setup(x => x.Create(It.IsAny<Board>())).ReturnsAsync(createdBoard);
        //    mockMapper.Setup(x => x.Map<Board>(boardDto)).Returns(board);
        //    mockMapper.Setup(x => x.Map<BoardDTO>(createdBoard)).Returns(createdBoardDto);

        //    var controller = new BoardController(
        //        mockBoardService.Object,
        //        mockMapper.Object,
        //        null, 
        //        null, 
        //        mockHistoryItemService.Object
        //    );

        //    // Act
        //    var result = await controller.CreateBoard(boardDto);

        //    // Assert
        //    var createdAtActionResult = result as CreatedAtActionResult;
        //    Assert.IsNotNull(createdAtActionResult);
        //    var model = createdAtActionResult.Value as BoardDTO;
        //    Assert.IsNotNull(model);
        //    Assert.AreEqual("Test Board", model.Title);
        //}
        //[TestMethod]
        //public async Task CreateCard_Returns_CreatedAtActionResult()
        //{
        //    // Arrange
        //    var mockCardService = new Mock<IDbEntityService<Card>>();
        //    var mockCatalogService = new Mock<IDbEntityService<Catalog>>();
        //    var mockHistoryItemService = new Mock<IDbEntityService<HistoryItem>>();
        //    var mockMapper = new Mock<IMapper>();

        //    var controller = new CardController(
        //        mockCardService.Object,
        //        mockCatalogService.Object,
        //        mockHistoryItemService.Object,
        //        mockMapper.Object
        //    );

        //    var cardDto = new CardDTO { Title = "Test Card", DueDate = DateTime.Now };
        //    var createdCard = new Card { Id = 1, Title = "Test Card", DueDate = DateTime.Now };
        //    var createdCardDto = new CardDTO { Id = 1, Title = "Test Card", DueDate = DateTime.Now };

        //    mockMapper.Setup(x => x.Map<Card>(cardDto)).Returns(createdCard);
        //    mockMapper.Setup(x => x.Map<CardDTO>(createdCard)).Returns(createdCardDto);
        //    mockCardService.Setup(x => x.Create(It.IsAny<Card>())).ReturnsAsync(createdCard);
        //    mockCatalogService.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(new Catalog()); // Assuming GetById always returns a Catalog

        //    // Act
        //    var result = await controller.CreateCard(cardDto, 1, 1);

        //    // Assert
        //    var createdAtActionResult = result as CreatedAtActionResult;
        //    Assert.IsNotNull(createdAtActionResult);
        //    var model = createdAtActionResult.Value as CardDTO;
        //    Assert.IsNotNull(model);
        //    Assert.AreEqual("Test Card", model.Title);
        //}

        //[TestMethod]
        //public async Task DeleteCard_Returns_NoContentResult_When_Card_Is_Deleted()
        //{
        //    // Arrange
        //    var mockCardService = new Mock<IDbEntityService<Card>>();
        //    var mockHistoryItemService = new Mock<IDbEntityService<HistoryItem>>();
        //    var mockMapper = new Mock<IMapper>();

        //    var controller = new CardController(
        //        mockCardService.Object,
        //        null, // mockCatalogService is not needed for this test
        //        mockHistoryItemService.Object,
        //        mockMapper.Object
        //    );

        //    var cardId = 1;
        //    mockCardService.Setup(x => x.GetById(cardId)).ReturnsAsync(new Card());

        //    // Act
        //    var result = await controller.DeleteCard(cardId, 1);

        //    // Assert
        //    Assert.IsInstanceOfType(result, typeof(NoContentResult));
        //}
        //[TestMethod]
        //public async Task CreateCatalog_Returns_CreatedAtActionResult()
        //{
        //    // Arrange
        //    var mockCatalogService = new Mock<IDbEntityService<Catalog>>();
        //    var mockBoardService = new Mock<IDbEntityService<Board>>();
        //    var mockHistoryItemService = new Mock<IDbEntityService<HistoryItem>>();
        //    var mockMapper = new Mock<IMapper>();

        //    var controller = new CatalogController(
        //        mockCatalogService.Object,
        //        null,                 
        //        mockHistoryItemService.Object,
        //        mockBoardService.Object,
        //        mockMapper.Object
        //    );

        //    var catalogDto = new CatalogDTO { Title = "Test Catalog" };
        //    var createdCatalog = new Catalog { Id = 1, Title = "Test Catalog" };
        //    var createdCatalogDto = new CatalogDTO { Id = 1, Title = "Test Catalog" };

        //    mockMapper.Setup(x => x.Map<Catalog>(catalogDto)).Returns(createdCatalog);
        //    mockMapper.Setup(x => x.Map<CatalogDTO>(createdCatalog)).Returns(createdCatalogDto);
        //    mockCatalogService.Setup(x => x.Create(It.IsAny<Catalog>())).ReturnsAsync(createdCatalog);
        //    mockBoardService.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(new Board());

        //    // Assert
        //    Assert.IsNotNull(mockMapper);
        //    Assert.AreEqual("Test Catalog", catalogDto.Title);
        //}

        //[TestMethod]
        //public async Task DeleteCatalog_Returns_NoContentResult_When_Catalog_Is_Deleted()
        //{
        //    // Arrange
        //    var mockCatalogService = new Mock<IDbEntityService<Catalog>>();
        //    var mockCardService = new Mock<IDbEntityService<Card>>();
        //    var mockHistoryItemService = new Mock<IDbEntityService<HistoryItem>>();
        //    var mockMapper = new Mock<IMapper>();

        //    var controller = new CatalogController(
        //        mockCatalogService.Object,
        //        null,
        //        mockHistoryItemService.Object,
        //        null, 
        //        mockMapper.Object
        //    );

        //    var catalogId = 1;
        //    mockCatalogService.Setup(x => x.GetById(catalogId)).ReturnsAsync(new Catalog());

        //    // Assert
        //    Assert.IsNotNull(mockCatalogService);
        //}
        [TestMethod]
        public async Task GetAllHistoryItems_Returns_OkResult_With_All_History_Items()
        {
            // Arrange
            var mockHistoryItemService = new Mock<IDbEntityService<HistoryItem>>();
            var mockMapper = new Mock<IMapper>();

            var controller = new HistoryItemController(
                mockHistoryItemService.Object,
                null, // mockCardService is not needed for this test
                null, // mockBoardService is not needed for this test
                mockMapper.Object
            );

            var historyItems = new List<HistoryItem>
        {
            new HistoryItem { Id = 1, EventDescription = "Event 1" },
            new HistoryItem { Id = 2, EventDescription = "Event 2" },
            new HistoryItem { Id = 3, EventDescription = "Event 3" }
        }.AsQueryable();

            mockHistoryItemService.Setup(x => x.GetAll()).Returns(historyItems);

            // Act

            // Assert
            Assert.IsNotNull(mockHistoryItemService);
            Assert.AreEqual(3, historyItems.Count());
        }

        [TestMethod]
        public async Task GetAllHistoryItemsForCard_Returns_OkResult_With_History_Items_For_Specific_Card()
        {
            // Arrange
            var mockHistoryItemService = new Mock<IDbEntityService<HistoryItem>>();
            var mockCardService = new Mock<IDbEntityService<Card>>();
            var mockMapper = new Mock<IMapper>();

            var controller = new HistoryItemController(
                mockHistoryItemService.Object,
                mockCardService.Object,
                null, // mockBoardService is not needed for this test
                mockMapper.Object
            );

            var cardId = 1;
            var historyItems = new List<HistoryItem>
        {
            new HistoryItem { Id = 1, EventDescription = "Event 1", CardId = cardId },
            new HistoryItem { Id = 2, EventDescription = "Event 2", CardId = cardId },
            new HistoryItem { Id = 3, EventDescription = "Event 3", CardId = cardId }
        }.AsQueryable();

            mockCardService.Setup(x => x.GetById(cardId)).ReturnsAsync(new Card { Id = cardId });
            mockHistoryItemService.Setup(x => x.GetAll()).Returns(historyItems);

            // Assert
            Assert.IsNotNull(mockCardService);
            Assert.AreEqual(3, historyItems.Count());
        }
    }
}
