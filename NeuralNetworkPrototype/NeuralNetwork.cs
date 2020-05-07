using System;
using System.Collections.Generic;

namespace NeuralNetworkPrototype
{
    public class NeuralNetwork
    {
        public Topology topology { get; }
        List<Layer> layers { get; }

        internal static bool bias;

        Random r = new Random();

        public NeuralNetwork(Topology topology, bool Isbias) // Инициализация слоев и весов
        {
            // Инициализация компонентов
            this.topology = topology;
            bias = Isbias;
            layers = new List<Layer>();

            // Добавление слоев
            var input = new Layer(this.topology.InputCount, LayerType.Input, topology);
            layers.Add(input);
            var output = new Layer(this.topology.OutputCount, LayerType.HiddenAndOutput,topology);

            for (int i = 0; i < this.topology.HiddenLayers.Length; i++)
            {
                var hidden = new Layer(this.topology.HiddenLayers[i], LayerType.HiddenAndOutput, topology);
                layers.Add(hidden);
            }

            layers.Add(output);

            Getweights();
        }

        private void Getweights() // Выдает всем нейронам рандомные веса
        {
            for(int i = 1; i < layers.Count; i++) // Для каждого слоя 
            {
                for(int j = 0; j < layers[i].size; j++) // Для каждого нейрона
                {
                        for(int k = 0; k < layers[i-1].size; k++) // Каждый вес скрытых нейронов
                        {
                            HiddenAndOutputNeuron hiddenNeuron = layers[i].neurons[j] as HiddenAndOutputNeuron;

                            if (hiddenNeuron != null)
                                hiddenNeuron.Weights.Add(r.NextDouble() -  0.5);
                            else
                                throw new Exception("Неверное преобразование скрытого нейрона");                             
                        }
                    if (bias) // Добавляет вес для призрачных нейронов(нейронов смещения)
                    {
                        HiddenAndOutputNeuron bias = layers[i].neurons[j] as HiddenAndOutputNeuron;

                        if (bias != null)
                            bias.Weights.Add(1);
                        else
                            throw new Exception("Неверное преобразование скрытого нейрона");
                    }
                }
            }
        }

        public void Learn(double learningrate, double expected)
        {
            Backpropagation(expected); // Вычисляет все ошибки
            WeightsAdjustment(learningrate); // Правит все веса 
        }

        private void Backpropagation(double expected)
        {
            for(int i = 0; i < layers[layers.Count - 1].size; i++)
            {
                layers[layers.Count - 1].neurons[i].BackPropagation(expected,true);
                var q = layers[layers.Count - 1].neurons[i] as HiddenAndOutputNeuron;
                Console.WriteLine(q.Error);
            }

                for (int numbeOfhiddenLayer = layers.Count - 2; numbeOfhiddenLayer > 0; numbeOfhiddenLayer--) // Каждый слой справа налево
                {
                    for (int numberOfNeuron = 0; numberOfNeuron < layers[numbeOfhiddenLayer].size; numberOfNeuron++) // Номер нейрона
                    {
                        for (int numberOfNeuronInNextLayer = 0; numberOfNeuronInNextLayer < layers[numbeOfhiddenLayer + 1].size; numberOfNeuronInNextLayer++) // Каждый нейрон следующешл слоя
                        {
                            var nextlayerneuron = layers[numbeOfhiddenLayer + 1].neurons[numberOfNeuronInNextLayer] as HiddenAndOutputNeuron;
                            if (nextlayerneuron != null)
                            {
                                double error = 0;
                                error += nextlayerneuron.Error * nextlayerneuron.Weights[numberOfNeuron];
                                layers[numbeOfhiddenLayer].neurons[numberOfNeuron].BackPropagation(error, false);
                            }
                            else throw new Exception("Неверно выбран нейрон следующего слоя");
                        }
                    }
                }
        }

        private void WeightsAdjustment(double learningrate) // Правит все веса
        {
            for(int i = 1; i < layers.Count; i++)
            {
                for(int j = 0; j < layers[i].size; j++)
                {
                    for(int k = 0; k < layers[i - 1].size; k++)
                    {
                        var hiddenOrOutputNeuron = layers[i].neurons[j] as HiddenAndOutputNeuron;
                        if (hiddenOrOutputNeuron != null)
                        {
                            hiddenOrOutputNeuron.Adjustment(learningrate, layers[i - 1].neurons);
                        }
                        else throw new Exception("Нейрон не является скрытым или выходным");
                    }
                }
            }
        }

        public double[] Use(params double[] inputs) // Вносишь данные - получаешь ответ от нейросети
        {
            try { SetSignalstoInput(inputs); }
            catch { throw new Exception("Количество нейронов не соответствует количеству входных данных"); }
            
            double[] previousOutputs = new double[layers[0].size];
            for(int i = 0; i < layers[0].size; i++)
            {
                previousOutputs[i] = (layers[0].neurons[i].Output);
            }

            var result = FeedForwardForRestOfLayers(previousOutputs);
            return result;
        }

        private void SetSignalstoInput(params double[] inputs) // Назначает входные-выходные данные для первого слоя нейронов
        {
            for(int i = 0; i < inputs.Length; i++)
            {
                layers[0].neurons[i].FeedForward(inputs[i]);
            }
        }

        private double[] FeedForwardForRestOfLayers(double[] previousOutputs) // Дает нейронам входные данные и получает выходные
        {
            for (int i = 1; i < layers.Count; i++)
            {
                for (int j = 0; j < layers[i].size; j++)
                {
                    layers[i].neurons[j].FeedForward(previousOutputs);
                }
                previousOutputs = new double[layers[i].size];
                for (int k = 0; k < previousOutputs.Length; k++)
                {
                    previousOutputs[k] = layers[i].neurons[k].Output;
                }
            }
            return previousOutputs;
        }
    }
}