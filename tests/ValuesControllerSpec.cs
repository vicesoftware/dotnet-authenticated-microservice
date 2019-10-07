using api.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace tests {
    public class ValuesControllerSpec{
        private readonly ValuesController sut;

        public ValuesControllerSpec()
        {
            sut = new ValuesController();
        }

        [Fact]
        public void get_all_values_returns_ok_result()
        {
            var okResult = sut.Get();
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void get_all_values_returns_3_items()
        {
            var result = sut.Get().Result as OkObjectResult;
            var items = Assert.IsType<string[]>(result.Value);
            Assert.Equal(3, items.Length);
        }
    }
}