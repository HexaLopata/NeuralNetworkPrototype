using System.Collections.Generic;

namespace NeuralNetworkPrototype
{
    internal interface INeuron
    {
        double Output { get; }

        void FeedForward(params double[] inputs); // Получает входные значения и вводит выходные
        void BackPropagation(double expected, bool isFirst);
    }
}
