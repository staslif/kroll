using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Чтение количества точек N из первой строки ввода
        int numberOfPoints = int.Parse(Console.ReadLine());

        // Создание списков для хранения точек в каждой из 4-х координатных четвертей
        List<(int x, int y)>[] pointsByQuadrant = new List<(int x, int y)>[4];
        for (int i = 0; i < 4; i++)
            pointsByQuadrant[i] = new List<(int x, int y)>();

        // Чтение координат N точек
        for (int i = 0; i < numberOfPoints; i++)
        {
            // Чтение строки с координатами и разделение на x и y
            string[] coordinates = Console.ReadLine().Split();
            int x = int.Parse(coordinates[0]);
            int y = int.Parse(coordinates[1]);

            // Пропуск точек, лежащих на осях (x=0 или y=0)
            if (x == 0 || y == 0)
                continue;

            // Распределение точек по четвертям:
            // 1-я четверть: x>0, y>0
            if (x > 0 && y > 0)
                pointsByQuadrant[0].Add((x, y));
            // 2-я четверть: x<0, y>0
            else if (x < 0 && y > 0)
                pointsByQuadrant[1].Add((x, y));
            // 3-я четверть: x<0, y<0
            else if (x < 0 && y < 0)
                pointsByQuadrant[2].Add((x, y));
            // 4-я четверть: x>0, y<0
            else if (x > 0 && y < 0)
                pointsByQuadrant[3].Add((x, y));
        }

        // Начальное предположение: 1-я четверть содержит максимальное количество точек
        int quadrantWithMaxPoints = 1;
        int maxPointsCount = pointsByQuadrant[0].Count;

        // Поиск четверти с максимальным количеством точек
        for (int i = 1; i < 4; i++)
        {
            // Если найдена четверть с большим количеством точек
            if (pointsByQuadrant[i].Count > maxPointsCount)
            {
                quadrantWithMaxPoints = i + 1;  // Нумерация четвертей с 1
                maxPointsCount = pointsByQuadrant[i].Count;
            }
            // Если количество точек одинаковое, выбираем по минимальному R
            else if (pointsByQuadrant[i].Count == maxPointsCount)
            {
                // Вычисление минимального R для текущей лучшей четверти
                int currentMinR = CalculateMinR(pointsByQuadrant[quadrantWithMaxPoints - 1]);
                // Вычисление минимального R для рассматриваемой четверти
                int newMinR = CalculateMinR(pointsByQuadrant[i]);

                // Если R меньше или при равенстве номер четверти меньше
                if (newMinR < currentMinR || (newMinR == currentMinR && i + 1 < quadrantWithMaxPoints))
                {
                    quadrantWithMaxPoints = i + 1;
                    maxPointsCount = pointsByQuadrant[i].Count;
                }
            }
        }

        // Получение списка точек в выбранной четверти
        var selectedQuadrantPoints = pointsByQuadrant[quadrantWithMaxPoints - 1];
        // Начальное предположение: первая точка в списке
        (int x, int y) closestPoint = selectedQuadrantPoints[0];
        // Вычисление R для первой точки (минимальное из |x| и |y|)
        int minDistance = Math.Min(Math.Abs(closestPoint.x), Math.Abs(closestPoint.y));

        // Поиск точки с минимальным R в выбранной четверти
        foreach (var point in selectedQuadrantPoints)
        {
            int currentDistance = Math.Min(Math.Abs(point.x), Math.Abs(point.y));
            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;
                closestPoint = point;
            }
        }

        // Вывод результатов
        Console.WriteLine($"K = {quadrantWithMaxPoints}");
        Console.WriteLine($"M = {maxPointsCount}");
        Console.WriteLine($"A = ({closestPoint.x}, {closestPoint.y})");
        Console.WriteLine($"A = ({A.x}, {A.y})");
        Console.WriteLine($"R = {R}");
    }

    // Вспомогательная функция для нахождения минимального R в списке точек
    static int GetMinR(List<(int x, int y)> points)
    {
        // Если список пуст, возвращаем максимально возможное значение
        if (points.Count == 0)
            return int.MaxValue;

        // Начальное предположение: R первой точки
        int minR = Math.Min(Math.Abs(points[0].x), Math.Abs(points[0].y));

        // Поиск минимального R среди всех точек
        foreach (var point in points)
        {
            int currentR = Math.Min(Math.Abs(point.x), Math.Abs(point.y));
            if (currentR < minR)
                minR = currentR;
        }

        return minR;
    }
}
