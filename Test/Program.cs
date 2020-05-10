using System;
using System.Collections.Generic;
using System.IO;
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
            double[][] output = new double[16][];
            output[0] = new double[2] { 0.8, 0.2 };
            output[1] = new double[2] { 0.8, 0.2 };
            output[2] = new double[2] { 0.8, 0.2 };
            output[3] = new double[2] { 0.2, 0.8 };
            output[4] = new double[2] { 0.2, 0.8 };
            output[5] = new double[2] { 0.2, 0.8 };
            output[6] = new double[2] { 0.8, 0.8};
            output[7] = new double[2] { 0.8, 0.8};
            output[8] = new double[2] { 0.8, 0.8};
            output[9] = new double[2] { 0.8, 0.8};
            output[10] = new double[2] { 0.8, 0.8 };
            output[11] = new double[2] { 0.8, 0.8 };
            output[12] = new double[2] { 0.8, 0.8 };
            output[13] = new double[2] { 0.8, 0.8 };
            output[14] = new double[2] { 0.8, 0.8 };
            output[15] = new double[2] { 0.2, 0.2 };

            double[][] input = new double[16][];
            input[0] = new double[9]
            {
                1,1,1,
                0,0,0,
                0,0,0
            };
            input[1] = new double[9]
{
                0,0,0,
                1,1,1,
                0,0,0
};
            input[2] = new double[9]
{
                0,0,0,
                0,0,0,
                1,1,1
};
            input[3] = new double[9]
{
                1,0,0,
                1,0,0,
                1,0,0
};
            input[4] = new double[9]
{
                0,1,0,
                0,1,0,
                0,1,0
};
            input[5] = new double[9]
{
                0,0,1,
                0,0,1,
                0,0,1
};
            input[6] = new double[9]
{
                1,1,1,
                1,0,0,
                1,0,0
};
            input[7] = new double[9]
{
                1,1,1,
                0,1,0,
                0,1,0
};
            input[8] = new double[9]
{
                1,1,1,
                0,0,1,
                0,0,1
};
            input[9] = new double[9]
{
                1,0,0,
                1,1,1,
                1,0,0
};
            input[10] = new double[9]
{
                0,1,0,
                1,1,1,
                0,1,0
};
            input[11] = new double[9]
{
                0,0,1,
                1,1,1,
                0,0,1
};
            input[12] = new double[9]
{
                1,0,0,
                1,0,0,
                1,1,1
};
            input[13] = new double[9]
{
                0,1,0,
                0,1,0,
                1,1,1
};
            input[14] = new double[9]
{
                0,0,1,
                0,0,1,
                1,1,1
};
            input[15] = new double[9]
{
                0,0,0,
                0,0,0,
                0,0,0
};


            Topology topology = new Topology(ActivationFuncType.Sigmoid, 9, 2, 4, 3);
            NeuralNetwork neuralNetwork = new NeuralNetwork(topology, false);
            for (int epoch = 0; epoch < 500; epoch++)
            {
                for (int i = 0; i < 16; i++)
                {
                    neuralNetwork.Use(input[i]);
                    neuralNetwork.Learn(0.1, output[i]);
                }
            }

            Console.ReadKey();
        }
    }
}
