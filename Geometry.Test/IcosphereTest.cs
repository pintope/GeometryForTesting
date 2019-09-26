namespace Geometry.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using GeometryForTesting.Geometry;
    using GeometryForTesting;


    [TestClass]
    public class IcosphereTest
    {
        private readonly double icosphereVolume = (4.0 / 3.0) * Math.PI * Math.Pow(1.0 * Math.Sin(2.0 * Math.PI / 5.0), 3.0);


        [TestInitialize]
        public void TestInitialize()
        {
            // Vacío en lo posible.
        }

        [TestMethod]
        public void Icosphere_GetVolume_CalculationOfVolumeIsCorrect()
        {
            // Arrange: Obtiene un icosaedro de lado 1.
            Icosphere icosphere = new Icosphere();

            // Act: Obtiene su volumen.
            double volume = icosphere.GetVolume();

            // Assert: El volumen es 4Π(1*Sen(2Π/5)³)/3.
            Assert.IsTrue(volume == icosphereVolume);
        }

        [TestMethod]
        public void Icosphere_GetVolume_MethodSinIsInvoked()
        {
            // Arrange: Obtiene un icosaedro de lado 1 y le pasa una clase de utilidades simulada.
            var toolMock = new Mock<MathTools>();
            Icosphere icosphere = new Icosphere();
            TestUtil.SetPrivateFieldOrProperty(icosphere, "tool", toolMock.Object);

            // Act: Obtiene su volumen.
            double volume = icosphere.GetVolume();

            // Assert: El método Sin() es invocado.
            toolMock.Verify(m => m.Sin(It.IsAny<double>()));
        }

        [TestMethod]
        public void Icosphere_GetVolume_PropertyPIIsAccessed()
        {
            // Arrange: Obtiene un icosaedro de lado 1 y le pasa una clase de utilidades simulada.
            var toolMock = new Mock<MathTools>();
            Icosphere icosphere = new Icosphere();
            TestUtil.SetPrivateFieldOrProperty(icosphere, "tool", toolMock.Object);

            // Act: Obtiene su volumen.
            double volume = icosphere.GetVolume();

            // Assert: La propiedad PI es accedida.
            toolMock.VerifyGet(m => m.PI);
        }

        [TestMethod]
        public void Icosphere_GetVolume_MethodMulIsInvokedNTimes()
        {
            // Arrange: Obtiene un icosaedro de lado 1 y le pasa una clase de utilidades simulada.
            var toolMock = new Mock<MathTools>();
            int calls = 0;
            toolMock.Setup(m => m.Mul(It.IsAny<double>(), It.IsAny<double>()))
                .Callback(() => calls++);

            Icosphere icosphere = new Icosphere();
            TestUtil.SetPrivateFieldOrProperty(icosphere, "tool", toolMock.Object);

            // Act: Obtiene su volumen.
            double volume = icosphere.GetVolume();

            // Assert: El método Mul() es llamado cuatro veces.
            Assert.IsTrue(calls == 4);
        }
    }
}