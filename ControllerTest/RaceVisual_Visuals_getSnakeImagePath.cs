using System.Windows.Media;
using Model;
using NUnit.Framework;
using RaceVisuals;

namespace ControllerTest {
	public class RaceVisual_Visuals_getSnakeImagePath {

		[SetUp]
		public void Setup() { }

		[Test]
		public void getSnakeImagePath_Orange() {
			string expectedReturnValue = "Media/Snakes/OrangeSnake.png";

			string returnValue = Visuals.GetSnakeImagePath(TeamColors.Orange);

			Assert.AreEqual(returnValue, expectedReturnValue);
		}
	}
}
