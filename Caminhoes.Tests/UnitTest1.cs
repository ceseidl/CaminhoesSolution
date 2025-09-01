using Caminhoes.Api.Controllers;
using Caminhoes.Api.Data;
using Caminhoes.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Caminhoes.Tests
{
    public class CaminhoesControllerTests
    {
        private CaminhoesController GetController(Mock<ICaminhaoRepository> mockRepo)
        {
            return new CaminhoesController(mockRepo.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsList()
        {
            var mockRepo = new Mock<ICaminhaoRepository>();
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Caminhao> { new Caminhao { CodigoChassi = "123", Cor = "Azul", Modelo = Modelo.FH, AnoFabricacao = 2020, Planta = Planta.Brasil } });
            var controller = GetController(mockRepo);
            var result = await controller.GetAll();
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var list = Assert.IsAssignableFrom<IEnumerable<Caminhao>>(ok.Value);
            Assert.Single(list);
        }

        [Fact]
        public async Task GetById_ReturnsCaminhao_WhenExists()
        {
            var mockRepo = new Mock<ICaminhaoRepository>();
            mockRepo.Setup(r => r.GetByIdAsync("abc")).ReturnsAsync(new Caminhao { CodigoChassi = "abc", Cor = "Preto", Modelo = Modelo.FM, AnoFabricacao = 2021, Planta = Planta.Suecia });
            var controller = GetController(mockRepo);
            var result = await controller.GetById("abc");
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var caminhao = Assert.IsType<Caminhao>(ok.Value);
            Assert.Equal("abc", caminhao.CodigoChassi);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenNotExists()
        {
            var mockRepo = new Mock<ICaminhaoRepository>();
            mockRepo.Setup(r => r.GetByIdAsync("notfound")).ReturnsAsync((Caminhao?)null);
            var controller = GetController(mockRepo);
            var result = await controller.GetById("notfound");
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedCaminhao()
        {
            var caminhao = new Caminhao { CodigoChassi = "novo", Cor = "Branco", Modelo = Modelo.VM, AnoFabricacao = 2022, Planta = Planta.Franca };
            var mockRepo = new Mock<ICaminhaoRepository>();
            mockRepo.Setup(r => r.AddAsync(caminhao)).ReturnsAsync(caminhao);
            var controller = GetController(mockRepo);
            var result = await controller.Create(caminhao);
            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdCaminhao = Assert.IsType<Caminhao>(created.Value);
            Assert.Equal("novo", createdCaminhao.CodigoChassi);
        }

        [Fact]
        public async Task Update_ReturnsOk_WhenExists()
        {
            var caminhao = new Caminhao { CodigoChassi = "edit", Cor = "Verde", Modelo = Modelo.FH, AnoFabricacao = 2023, Planta = Planta.EstadosUnidos };
            var mockRepo = new Mock<ICaminhaoRepository>();
            mockRepo.Setup(r => r.UpdateAsync("edit", caminhao)).ReturnsAsync(caminhao);
            var controller = GetController(mockRepo);
            var result = await controller.Update("edit", caminhao);
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var updated = Assert.IsType<Caminhao>(ok.Value);
            Assert.Equal("edit", updated.CodigoChassi);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenNotExists()
        {
            var caminhao = new Caminhao { CodigoChassi = "none", Cor = "Rosa", Modelo = Modelo.FM, AnoFabricacao = 2024, Planta = Planta.Brasil };
            var mockRepo = new Mock<ICaminhaoRepository>();
            mockRepo.Setup(r => r.UpdateAsync("none", caminhao)).ReturnsAsync((Caminhao?)null);
            var controller = GetController(mockRepo);
            var result = await controller.Update("none", caminhao);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenDeleted()
        {
            var mockRepo = new Mock<ICaminhaoRepository>();
            mockRepo.Setup(r => r.DeleteAsync("del")).ReturnsAsync(true);
            var controller = GetController(mockRepo);
            var result = await controller.Delete("del");
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenNotExists()
        {
            var mockRepo = new Mock<ICaminhaoRepository>();
            mockRepo.Setup(r => r.DeleteAsync("none")).ReturnsAsync(false);
            var controller = GetController(mockRepo);
            var result = await controller.Delete("none");
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
