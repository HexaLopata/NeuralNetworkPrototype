namespace NeuralNetworkPrototype
{
    public class Topology
    {
        public int InputCount { get; }
        public int OutputCount { get; }
        public int[] HiddenLayers;

        public ActivationFuncType ActivationFunc { get; }

        public Topology(ActivationFuncType activationFunc, int inputneurons, int outputneurons, params int[] hiddenneurons)
        {
            InputCount = inputneurons;
            OutputCount = outputneurons;
            HiddenLayers = hiddenneurons;
            ActivationFunc = activationFunc;
        }
    }
}