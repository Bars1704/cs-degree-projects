using FourInRow;
using MyNeyralNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AICore
{
    public int LearnCount;
    readonly FourInRowGame game;
    readonly NeyralNetwork neyralNetwork;

#pragma warning disable IDE0044 // Добавить модификатор только для чтения
    List<DataExample> RedDataset;
    List<DataExample> YellowDataset;
#pragma warning restore IDE0044 // Добавить модификатор только для чтения
    public AICore(FourInRowGame fourInGame, int learnCount)
    {
        game = fourInGame;
        LearnCount = learnCount;
        RedDataset = new List<DataExample>();
        YellowDataset = new List<DataExample>();

        neyralNetwork = new NeyralNetwork(
            new NeyralNetworkConfig(0.5, 0.5), 
            new int[]{ 
            game.FieldSize.X * game.FieldSize.Y * 2,
            168, 84, 42, 20,
            game.FieldSize.X 
        });
    }
    public void Learn(CellInfo winner)
    {
        var dataset = winner == CellInfo.Red ? RedDataset : YellowDataset;
        for (int i = 0; i < LearnCount; i++)
        {
            foreach (var data in dataset)
                neyralNetwork.Learn(data);
        }
        YellowDataset.Clear();
        RedDataset.Clear();
    }
    public int GetStepCoord()
    {
        var x = AIPredict(out int resultindex);
        if (game.CurrentPlayer == CellInfo.Yellow)
        {
            YellowDataset.Add(x);
        }
        else
        {
            RedDataset.Add(x);
        }
        return resultindex;
    }
    double[] FieldToDataSet()
    {
        CellInfo current = game.CurrentPlayer;
        double[] result = new double[game.FieldSize.X * game.FieldSize.Y * 2];
        int counter = 0;
        for (int i = 0; i < game.FieldSize.X; i++)
        {
            for (int j = 0; j < game.FieldSize.Y; j++)
            {
                result[counter] = game.Field[i, j] == current ? 1 : 0;
                counter++;
            }
        }
        for (int i = 0; i < game.FieldSize.X; i++)
        {
            for (int j = 0; j < game.FieldSize.Y; j++)
            {
                result[counter] =
                    game.Field[i, j] == current || game.Field[i, j] == CellInfo.Blank
                    ? 0 : 1;
                counter++;
            }
        }
        return result;
    }
    DataExample AIPredict(out int index)
    {
        double[] inputData = FieldToDataSet();
        var result = neyralNetwork.GetResult(inputData).ToList();
        index = result.ToList().IndexOf(result.Max());
        if (game.Field[index, game.FieldSize.Y - 1] == CellInfo.Blank)
        {
            result[index] = 1;
            return new DataExample(inputData, result.ToArray());
        }
        else
        {
            result[index] = 0;
            var data = new DataExample(inputData, result.ToArray());
            for (int i = 0; i < LearnCount; i++)
            {
                neyralNetwork.Learn(data);
            }
            return AIPredict(out index);
        }
    }

    public void IncorrectIndexes(int x)
    {
        var inputData = AIPredict(out int result);
        if (result != x)
        {
            return;
        }
        inputData.Output[x] = 0;
        for (int i = 0; i < LearnCount; i++)
        {
            neyralNetwork.Learn(inputData);
        }
    }
}
