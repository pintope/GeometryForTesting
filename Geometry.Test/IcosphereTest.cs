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
        // El volumen de una icoesferea es 4Π(1*Sen(2Π/5)³)/3.
        private readonly double icosphereVolume = (4.0 / 3.0) * Math.PI * Math.Pow(1.0 * Math.Sin(2.0 * Math.PI / 5.0), 3.0);


        [TestInitialize]
        public void TestInitialize()
        {
            // TO-DO.
        }

        [TestMethod]
        public void Icosphere_GetVolume_CalculationOfVolumeIsCorrect()
        {
            // TO-DO.
        }

        [TestMethod]
        public void Icosphere_GetVolume_MethodSinIsInvoked()
        {
            // TO-DO.
        }

        [TestMethod]
        public void Icosphere_GetVolume_PropertyPIIsAccessed()
        {
            // TO-DO.
        }

        [TestMethod]
        public void Icosphere_GetVolume_MethodMulIsInvokedNTimes()
        {
            // TO-DO.
        }
    }
}