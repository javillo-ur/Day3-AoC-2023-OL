﻿Console.WriteLine(new List<List<string>>() { File.ReadAllLines("input.txt").ToList() }
    .Select(w => new {
        rows = w,
        numbers = w.Select((txt, row) => new { txt, row })
    .SelectMany(row => row.txt.Select((c, column) => new { c, column, row.row, row.txt })
    .Where(c => char.IsDigit(c.txt[c.column]) && (c.txt.Length == c.column + 1 || !char.IsDigit(c.txt[c.column + 1])))
    .Aggregate(new List<Tuple<int, int, string>>() as IEnumerable<Tuple<int, int, string>>, (previous, current) => previous.Append(new Tuple<int, int, string>(current.row, current.column, current.txt[(previous.LastOrDefault(new Tuple<int, int, string>(0, current.txt.IndexOf(current.txt.First(c => char.IsDigit(c))) - 1, null)).Item2 + 1)..(current.column + 1)])))
    ).Select(j => new Tuple<int, int, string>(j.Item1, j.Item2, j.Item3.Where(c => char.IsDigit(c)).Aggregate("", (previous, current) => previous + current)))
    .Select(j => new { Row = j.Item1, Start = j.Item2 - j.Item3.Length + 1, End = j.Item2, Value = int.Parse(j.Item3) }).ToList()
    }).Select(w =>
    w.numbers.Where(j => (j.Row > 0 && w.rows[j.Row - 1].Skip(j.Start - 1).SkipLast(w.rows[j.Row - 1].Length - (j.End + 2)).Any(c => !char.IsDigit(c) && c != '.')) ||
                         (j.Row < w.rows.Count - 1 && w.rows[j.Row + 1].Skip(j.Start - 1).SkipLast(w.rows[j.Row + 1].Length - (j.End + 2)).Any(c => !char.IsDigit(c) && c != '.')) ||
                         (j.Start > 0 && w.rows[j.Row][j.Start - 1] != '.') ||
                         (j.End < w.rows[j.Row].Length - 1 && w.rows[j.Row][j.End + 1] != '.')))
    .Sum(j => j.Sum(r => r.Value)));