using AutoMapper;
using BussinesLayer.Repositories.Base;
using DatabaseLayer.Models.Base;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiBase.Test.BaseConfiguration
{
    [TestClass]
    public abstract class CoreTest<TContext, TEntity, TInput, TEntityVM>
        where TEntity : BaseModel, new()
        where TInput : BaseModel
        where TEntityVM : BaseModel
        where TContext : DbContext
    {
        protected IBaseRepository<TInput, TEntityVM> _service;
        protected IMapper _mapper;
        protected abstract void Initialize();

        public IMapper SetUpMapper(Profile[] profiles)
        {
            var config = new MapperConfiguration(_ =>
            {
                foreach (var item in profiles)
                {
                    _.AddProfile(item);
                }
            });
            _mapper = config.CreateMapper();
            return _mapper;
        }

        public virtual List<TEntity> GetFakeDataList()
        {
            A.Configure<TEntity>().Fill(x => x.IsDeleted, () => false);
            var list = A.ListOf<TEntity>(10);
            return list;
        }

        public TEntity GetFakeCreationData() => GetFakeDataList()[0];

        public Guid GetFakeId()
        {
            var list = Task.Run(async () => await _service.GetList()).Result;
            return list.FirstOrDefault().Id;
        }

        public DbContextOptions<TContext> GetDbOptions()
        {
            var options = new DbContextOptionsBuilder<TContext>()
                .UseInMemoryDatabase("test")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            return options;
        }

        [TestMethod]
        public async Task GetAllTest()
        {
            var result = await _service.GetList();
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public async Task GetByIdNotNullTest()
        {         
            var result = await _service.GetById(GetFakeId());
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetByIdNull()
        {
            var result = await _service.GetById(Guid.NewGuid());
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task AddSuccessTest()
        {
            var data = _mapper.Map<TEntity, TInput>(GetFakeCreationData());
            var result = await _service.Create(data);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public async Task UpdateSuccessTest()
        {
            var data = _mapper.Map<TEntity, TInput>(GetFakeCreationData());
            data.Id = GetFakeId();
            var result = await _service.Update(data);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public async Task UpdateErrorTest()
        {
            var data = _mapper.Map<TEntity, TInput>(GetFakeCreationData());
            data.Id = Guid.NewGuid();
            var result = await _service.Update(data);
            Assert.IsTrue(!result.IsSuccess);
        }

        [TestMethod]
        public async Task RemoveFailTest()
        {
            var result = await _service.Remove(Guid.NewGuid());
            Assert.IsTrue(!result.IsSuccess);
        }

        [TestMethod]
        public async Task RemoveSuccessTest()
        {
            var result = await _service.Remove(GetFakeId());
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public async Task SoftRemoveSuccessTest()
        {
            var result = await _service.SoftRemove(GetFakeId());
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public async Task SoftRemoveFailTest()
        {
            var result = await _service.SoftRemove(Guid.NewGuid());
            Assert.IsTrue(!result.IsSuccess);
        }
    }
}
