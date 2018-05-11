using Moq;
using NUnit.Framework;
using System;

namespace Trivia
{
    public class PlayerLocationServiceTests
    {
        [Test]
        public void GivenFreshlyInitializedService_WithNoPlayers_RetrievingCurrentPlayerLocation_Throws()
        {
            var playerServiceMock = new Mock<IPlayerService>();
            playerServiceMock.SetupGet(ps => ps.CurrentPlayerIndex).Returns(0);
            var playerLocationService = new PlayerLocationService(playerServiceMock.Object);

            Assert.Throws<InvalidOperationException>(() => playerLocationService.GetCurrentPlayerLocation());
        }

        [Test]
        public void GivenFreshlyInitializedService_WithNoPlayers_SettingCurrentPlayerLocation_Throws()
        {
            var playerServiceMock = new Mock<IPlayerService>();
            playerServiceMock.SetupGet(ps => ps.CurrentPlayerIndex).Returns(0);
            var playerLocationService = new PlayerLocationService(playerServiceMock.Object);

            Assert.Throws<InvalidOperationException>(() => playerLocationService.SetCurrentPlayerLocation(1));
        }
    }
}
