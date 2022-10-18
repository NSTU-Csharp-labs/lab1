﻿using System;
using Generators;
namespace Program;

public class Program
{
    static List<ConstStepGen> ProgramConstStepGens = new List<ConstStepGen>();
    static List<RandomGen> ProgramRandomGens = new List<RandomGen>();
    static List<CompositionGen> ProgramCompositionGens = new List<CompositionGen>();

    static void Main(string[] args)
    {


        PrintStartMessage();
        char input = 'f';
        while (input != 'q')
        {
            PrintMenuOptions();
            input = UtilsForGenerators.Read("Введите команду:", 2)[0];
            switch (input)
            {

                case '0':
                    MenuPointHowProgramWorks();
                    break;
                case '1':
                    PrintGensInProgram();
                    break;
                case '2':
                    MenuPointAddDelGen();
                    break;
                case '3':
                    break;
                case '4':
                    break;
                case '5':
                    break;
                case 'q':
                    break;
                default:
                    Console.WriteLine("\nНеизвестная команда\n\n");
                    break;
            }

        }
    }

    public static void PrintGensInProgram()
    {
        Console.WriteLine("Генераторы с постоянным шагом:");
        PrintGens(ProgramConstStepGens.AsEnumerable());
        Console.WriteLine("Генераторы псевдослучайных чисел:");
        PrintGens(ProgramRandomGens.AsEnumerable());
        Console.WriteLine("Композитные генераторы:");
        bool CompositionGenIsFull = PrintGens(ProgramCompositionGens.AsEnumerable());
        //   if (CompositionGenIsFull)
        //       PrintGensInCompositeGen(ProgramCompositionGens);
    }

    public static bool PrintGens(IEnumerable<BaseGen> gen)
    {
        bool res = true;
        if (gen.Any())
        {
            foreach (var item in gen)
                Console.WriteLine("Имя генератора: " + item.Name);
        }
        else
        {
            Console.WriteLine("*Генераторы отсуствуют");
            res = false;
        }
        return res;
    }

    public static void PrintGensInCompositeGen(List<CompositionGen> gen)
    {
        Console.WriteLine("ERROR");
        //foreach (var item in gen)
        //Console.WriteLine($"В композитном генераторе с именем {item.Name} находятся генераторы:");
    }




    public static void MenuPointAddDelGen()
    {
        char input = 'a';

        while (input != 'q')
        {
            PrintOptionsAddDelGen();
            input = UtilsForGenerators.Read("Введите команду:", 2)[0];
            switch (input)
            {
                case '0':
                    ProgramConstStepGens.Add(UtilsForGenerators.CreateConstStepGen());
                    break;
                case '1':
                    ProgramRandomGens.Add(UtilsForGenerators.CreateRandomGen());
                    break;
                case '2':
                    ProgramCompositionGens.Add(UtilsForGenerators.CreateCompositionGen());
                    break;
                case '3':
                    AddGenToProgramCompositGen();
                    break;
                case '4':
                    DelGenInProgramCompositGen();
                    break;
                case 'q':
                    break;
                default:
                    Console.WriteLine("\nНеизвестная команда");
                    break;
            }

        }
    }


    public static void AddGenToProgramCompositGen()
    {
        if (ProgramCompositionGens.Any())
        {
            string GenNameToAdd = UtilsForGenerators.Read("Введите имя композитного генератора, в который будет добавляться генератор:", 2);
            char genDecision = UtilsForGenerators.Read("Вы хотите добавить генератор c постоянным шагом? y/n:", 2)[0];
            if (genDecision == 'y') GetProgramCompositGenByName(GenNameToAdd).PushGen(UtilsForGenerators.CreateConstStepGen());
            else GetProgramCompositGenByName(GenNameToAdd).PushGen(UtilsForGenerators.CreateRandomGen());
        }
        else
        {
            Console.WriteLine("Нет созданных компизитных генераторов");

        }
    }

    public static void DelGenInProgramCompositGen()
    {
        if (ProgramCompositionGens.Any())
        {
            string CompositGenNameToWork = UtilsForGenerators.Read("Введите имя композитного генератора, в котором будет удаляться:", 2);
            string GenNameToDoOperation = UtilsForGenerators.Read("Введите имя генератора, который хотите удалить из композитного:", 2);
            try
            {
                GetProgramCompositGenByName(CompositGenNameToWork).DeleteGenByName(GenNameToDoOperation);
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Генератор с таким именем не был найден");
            }
        }
        else
        {
            Console.WriteLine("Нет созданных компизитных генераторов");
        }
    }


    private static CompositionGen GetProgramCompositGenByName(string name)
    {
        return ProgramCompositionGens[ProgramCompositionGens.FindIndex(gen => gen.Name == name)];
    }


    public static void PrintOptionsAddDelGen() => Console.WriteLine("\t0 - Добавить генератор с постоянным шагом\n" +
                                                                     "\t1 - Добавить генератор псевдослучаных чисел\n" +
                                                                     "\t2 - Добавить композитный генератор\n" +
                                                                     "\t3 - Добавить генератор в композитный генератор" +
                                                                     "\t4 - Удалить генератор из композитного генератора\n" +
                                                                     "\tq - Выйти из данного пункта меню");



    public static void PrintOptionsHowProgramWorks() => Console.WriteLine("\t0 - Как работает генератор с постоянным шагом\n" +
                                                                           "\t1 - Как работает генератор псевдослучаных чисел\n" +
                                                                           "\t2 - Как работает композитный генератор\n" +
                                                                           "\t3 - В целом о программе и работе генераторов\n" +
                                                                           "\tq - Выйти из данного пункта меню");


    public static void PrintMenuOptions() => Console.WriteLine("\n\nМеню программы:\n" +
                                                           "\t0 - Как работают проргамма и генераторы\n" +
                                                           "\t1 - Вывод списка генераторов\n" +
                                                           "\t2 - Добавление/удаление генератора\n" +
                                                           "\t3 - Подсчет среднего числа у генератора\n" +
                                                           "\t4 - Добавление генератора в композитный генератор\n" +
                                                           "\t5 - Удаление генератора из композитного генератора(по имени/индексу)\n" +
                                                           "\tq - Выход\n");



    public static void MenuPointHowProgramWorks()
    {
        char input = 'o';
        while (input != 'q')
        {
            PrintOptionsHowProgramWorks();
            input = UtilsForGenerators.Read("Введите команду:", 2)[0];
            Console.WriteLine();
            switch (input)
            {
                case '0':
                    PrintHowConsStepGenWorks();
                    break;
                case '1':
                    PrintHowRandGenWorks();
                    break;
                case '2':
                    PrintHowCompositGenWorks();
                    break;
                case '3':
                    PrintHowGensWorks();
                    break;
                case 'q':
                    break;
                default:
                    Console.WriteLine("\nНеизвестная команда");
                    break;
            }
        }
    }

    public static void PrintHowConsStepGenWorks() => Console.WriteLine("(Const)Генератор с постоянным шагом создает числа от начальной позиции, прибавляя некотрое число.");

    public static void PrintHowRandGenWorks() => Console.WriteLine("(Rand)Генератор псевдослучайных чисел создает числа с помощью *ВоЛшЕбНоГо* алгоритма.");

    public static void PrintHowCompositGenWorks() => Console.WriteLine("(Composite)Композитный генератор - генератор, сосотящий из других генераторов. Можно добавлять соответственно другие генераторы\n" +
                                                                        "(Composite)Подсчет среднего арифметического числа соответственно считает средние всех вложенных генераторов и считает их среднее");

    public static void PrintStartMessage() => Console.WriteLine("Вас приветсвует программа по генерированию случайных чисел!\n" +
                                                        "Доступны 3 вида генераторов чисел:\n" +
                                                        "\t1 - Генератор с постоянным шагом\n" +
                                                        "\t2 - Генератор псевдослучайных чисел\n" +
                                                        "\t3 - Композитный генератор");

    public static void PrintHowGensWorks()
    {
        Console.WriteLine("У каждого генератора есть:\n" +
                        "\t1 - имя\n" +
                        "\t2 - некоторая константа N, от которой зависит подсчет среднего значения генератора: либо среднее последних N чисел, либо среднее всех имеющихся чисел\n" +
                        "\t3 - его режим работы при недостатке сгенерированных чисел: случай когда чисел меньше N, случай когда их столько же и когда больше\n" +
                        "\t4 - возможность добавления нового числа в генератор и подсчет среднего значения в соответствии с режимом работы и константой N\n");
        PrintHowConsStepGenWorks();
        PrintHowRandGenWorks();
        PrintHowCompositGenWorks();
    }


}