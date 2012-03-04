﻿using Encog.MathUtil.Matrices;
using Encog.ML.Data;
using Encog.ML.Data.Basic;
using Encog.Neural.SOM;
using Encog.Neural.SOM.Training.Neighborhood;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Encog.Neural.Networks.Training.Competitive
{
    [TestClass]
    public class TestCompetitive
    {
        public static double[][] SOMInput = {
                                                 new[] {0.0, 0.0, 1.0, 1.0},
                                                 new[] {1.0, 1.0, 0.0, 0.0}
                                             };

        // Just a random starting matrix, but it gives us a constant starting point
        public static double[][] MatrixArray = {
                                                    new[]
                                                        {
                                                            0.9950675732277183, -0.09315692732658198, 0.9840257865083011,
                                                            0.5032129897356723
                                                        },
                                                    new[]
                                                        {
                                                            -0.8738960119753589, -0.48043680531294997, -0.9455207768842442,
                                                            -0.8612565984447569
                                                        }
                                                };

        [TestMethod]
        public void TestSOM()
        {
            // create the training set
            IMLDataSet training = new BasicMLDataSet(
                SOMInput, null);

            // Create the neural network.
            var network = new SOMNetwork(4, 2) {Weights = new Matrix(MatrixArray)};

            var train = new BasicTrainSOM(network, 0.4,
                                          training, new NeighborhoodSingle()) {ForceWinner = true};
            int iteration = 0;

            for (iteration = 0; iteration <= 100; iteration++)
            {
                train.Iteration();
            }

            IMLData data1 = new BasicMLData(
                SOMInput[0]);
            IMLData data2 = new BasicMLData(
                SOMInput[1]);

            int result1 = network.Classify(data1);
            int result2 = network.Classify(data2);

            Assert.IsTrue(result1 != result2);
        }
    }
}