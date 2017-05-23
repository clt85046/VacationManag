using BAL;
using BAL.Manager;
using DAL.Common.Enum;
using DAL.Interface;
using DAL.Model;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationManagment.Test.BAL
{
	[TestFixture]
	public class ManagerManager_Test
	{
		[Test]
		public void ManagerManager_ApproveRequest()
		{
			AutoMapperConfig.Configure();
			// mock Repo logic
			var vacationRepo = new Mock<IGenericRepository<VacationRequest>>();
			var userRepo = new Mock<IGenericRepository<User>>();
			var uoW = new Mock<IUnitOfWork>();
			uoW.Setup(x => x.VacationRepo).Returns(vacationRepo.Object);
			uoW.Setup(x => x.UserRepo).Returns(userRepo.Object);

			var managerManager = new ManagerManager(uoW.Object);

			var dbStub = new VacationRequest()
			{
				Id = 1,
				UserId = 2,
				StartDate = new DateTime(2017, 05, 20)
			};
			var userStub = new User()
			{
				Id=2,
				LastName="Tarbinskyi",
				FirstName="Viacheslav"
			};
			userRepo.Setup(x => x.GetByID(It.IsAny<int>())).Returns(userStub);
			vacationRepo.Setup(x => x.GetByID(It.IsAny<int>())).Returns(dbStub);
			managerManager.ApproveRequest(1,2);
			Assert.AreEqual(dbStub.ApprovedBy,"Tarbinskyi Viacheslav");
			Assert.AreEqual(dbStub.Status,Status.Approved);
		}
	}
}
