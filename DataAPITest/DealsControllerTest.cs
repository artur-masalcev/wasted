using System.Collections.Generic;
using AutoMapper;
using DataAPI;
using DataAPI.Controllers;
using DataAPI.Models;
using DataAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Moq;
using NUnit.Framework;

namespace DataAPITest
{
    public class DealsControllerTest
    {
        private readonly MockRepository _mockRepository;
        private readonly IMapper _mapper;
        private readonly DealsController _dealsController;
        private readonly Mock<IDealsRepository> _dealsRepository;
        private readonly Deal _deal = new Deal();

        public DealsControllerTest()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _mapper = new MapperConfiguration(config => config.AddProfile<MappingProfile>())
                    .CreateMapper();
            _dealsRepository = _mockRepository.Create<IDealsRepository>();
            _dealsController = new DealsController(_dealsRepository.Object, _mapper);
        }

        [Test]
        public void ShouldReturnGivenDeal()
        {
            _dealsRepository.Setup(r => r.Update(_deal)).Verifiable();
            Deal deal = _dealsController.UpdateDeal(_deal).Value;
            
            Assert.AreEqual(deal, _deal);
            _dealsRepository.Verify(d => d.Update(deal), Times.Once);
        }
    }
}