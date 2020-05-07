using System.Collections.Generic;

namespace NeuralNetworkPrototype
{
    class InputNeuron : INeuron
    {
        Topology topology;
        public double Output { get; private set; }

        public InputNeuron(Topology topology)
        {
            this.topology = topology;
        }

        public void FeedForward(params double[] input)
        {
            Output = input[0];
        }

        public void BackPropagation(double expected, bool isFirst)
        {
            throw new System.NotImplementedException();
        }
    }
}