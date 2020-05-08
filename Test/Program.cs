using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetworkPrototype;

namespace Test
{
    class Program
    {
        static void Main()
        {

            double[] output = new double[] { 0, 1, 1, 0 };
            double[][] input = new double[4][];
            input[0] = new double[] { 0, 0, 1 };
            input[1] = new double[] { 1, 1, 1 };
            input[2] = new double[] { 1, 0, 1 };
            input[3] = new double[] { 0, 1, 1 };

            Topology topology = new Topology(ActivationFuncType.Sigmoid, 3, 1, 1);
            NeuralNetwork neuralNetwork = new NeuralNetwork(topology, true);

            for (int epoch = 0; epoch < 50000; epoch++)
            {
                for (int i = 0; i < 4; i++)
                {
                    neuralNetwork.Use(input[i]);
                    neuralNetwork.Learn(0.01, output[i]);
                }
            }
            for (int i = 0; i < topology.OutputCount; i++)
            {
                Console.WriteLine(neuralNetwork.Use(1, 0, 0)[i]);
            }
            Console.ReadKey();
        }
    }
}
