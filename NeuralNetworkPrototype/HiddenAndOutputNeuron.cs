using System.Collections.Generic;

namespace NeuralNetworkPrototype
{
    class HiddenAndOutputNeuron : INeuron
    {
        Topology topology;
        public double Output { get; private set; }
        public List<double> Weights { get; set; }
        public double Error { get; set; }

        public HiddenAndOutputNeuron(Topology topology)
        {
            Weights = new List<double>();
            this.topology = topology;
        }

        public void FeedForward(params double[] inputs)
        {
            double sum = 0;
            if (NeuralNetwork.bias) // Опционально добавляет влияние нейрона смещения
                sum = 1 * Weights[Weights.Count - 1];

            for(int i = 0; i < inputs.Length; i++)
            {
                sum += inputs[i] * Weights[i];
            }

            Output = ActivationFunctions.Use(topology.ActivationFunc, sum);
        }

        public void BackPropagation(double value, bool isFirst)
        {
            if (isFirst)
            {
                Error = value - Output;
            }
            else
            {
                Error = value;
            }
        }

        public void Adjustment(double learningrate, List<INeuron> neurons)
        {
            for(int i = 0; i < neurons.Count; i++)
            {
                Weights[i] = Weights[i] + (Error * ActivationFunctions.UseDX(topology.ActivationFunc, Output) * neurons[i].Output * learningrate);
            }

            if(NeuralNetwork.bias) // Корректировка веса для нейрона смещения
                Weights[Weights.Count - 1] = Weights[Weights.Count - 1] + (Error * ActivationFunctions.UseDX(topology.ActivationFunc, Output) * 1 * Weights[Weights.Count - 1] * learningrate);
        }
    }
}