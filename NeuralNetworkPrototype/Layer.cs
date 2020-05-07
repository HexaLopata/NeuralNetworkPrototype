using System.Collections.Generic;

namespace NeuralNetworkPrototype
{
    internal class Layer
    {
        public List<INeuron> neurons;
        public int size;

        public Layer(int size, LayerType type, Topology topology)
        {
            neurons = new List<INeuron>();
            this.size = size;

            switch(type)
            {
                case LayerType.Input:
                    for(int i = 0; i < size; i++)
                    {
                        var neuron = new InputNeuron(topology);
                        neurons.Add(neuron);
                    }
                    break;
                case LayerType.HiddenAndOutput:
                    for (int i = 0; i < size; i++)
                    {
                        var neuron = new HiddenAndOutputNeuron(topology);
                        neurons.Add(neuron);
                    }
                    break;
            }
        }
    }
}