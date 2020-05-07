using System;

namespace NeuralNetworkPrototype
{
    static class ActivationFunctions
    {
        public static double Use(ActivationFuncType type, double x) // Возвращает результат выбранной функции
        {
            switch (type)
            {
                case ActivationFuncType.Sigmoid:
                    double result = (1 / (1 + Math.Pow(Math.E, -x)));
                    return result;
                default:
                    throw new Exception("Функция активации использована неверно");
            }
        }

        public static double UseDX(ActivationFuncType type, double output)
        {
            switch (type)
            {
                case ActivationFuncType.Sigmoid:
                    double result = output * (1 - output);
                    return result;
                default:
                    throw new Exception("Производная функции активации использована неверно");
            }
        }
    }
}