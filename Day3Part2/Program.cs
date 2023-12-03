﻿Console.WriteLine(new List<List<string>>() { File.ReadAllLines("input.txt").ToList() }
    .Select(w => new {
        rows = w,
        numbers = w.Select((txt, row) => new { txt, row })
    .SelectMany(row => row.txt.Select((c, column) => new { c, column, row.row, row.txt })
    .Where(c => char.IsDigit(c.txt[c.column]) && (c.txt.Length == c.column + 1 || !char.IsDigit(c.txt[c.column + 1])))
    .Aggregate(new List<Tuple<int, int, string>>() as IEnumerable<Tuple<int, int, string>>, (previous, current) => previous.Append(new Tuple<int, int, string>(current.row, current.column, current.txt[(previous.LastOrDefault(new Tuple<int, int, string>(0, current.txt.IndexOf(current.txt.First(c => char.IsDigit(c))) - 1, null)).Item2 + 1)..(current.column + 1)])))
    ).Select(j => new Tuple<int, int, string>(j.Item1, j.Item2, j.Item3.Where(c => char.IsDigit(c)).Aggregate("", (previous, current) => previous + current)))
    .Select(j => new { Row = j.Item1, Start = j.Item2 - j.Item3.Length + 1, End = j.Item2, Value = int.Parse(j.Item3) }).ToList()
    }).Select(w => w.rows.SelectMany((str, row) => str.Select((character, column) => new { character, column, row }))
    .Where(j => j.character == '*').Where(j =>
        w.numbers.Count(t => t.Row - 1 <= j.row && t.Row + 1 >= j.row && t.Start - 1 <= j.column && t.End + 1 >= j.column) == 2)
    .Sum(j => w.numbers.Where(t => t.Row - 1 <= j.row && t.Row + 1 >= j.row && t.Start - 1 <= j.column && t.End + 1 >= j.column).Min(t => t.Value) *
              w.numbers.Where(t => t.Row - 1 <= j.row && t.Row + 1 >= j.row && t.Start - 1 <= j.column && t.End + 1 >= j.column).Max(t => t.Value))).ToList()[0]);