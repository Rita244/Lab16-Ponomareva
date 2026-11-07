// ### Часть 1. Линейный поиск


// int[] numbers = [7, 14, 3, 25, 9, 18];

// int idx1 = FindLinear(numbers, 7);
// Console.WriteLine(idx1 >= 0 ? $"Найден, индекс {idx1}" : "Не найден");

// int idx2 = FindLinear(numbers, 18);
// Console.WriteLine(idx2 >= 0 ? $"Найден, индекс {idx2}" : "Не найден");

// int idx3 = FindLinear(numbers, 99);
// Console.WriteLine(idx3 >= 0 ? $"Найден, индекс {idx3}" : "Не найден");

// int FindLinear(int[] numbers, int target)
// {
//     for (int i = 0; i < numbers.Length; i++)
//     {
//         if (numbers[i] == target)
//         {
//             return i;
//         }
//     }

//     return -1;
// }


// ### Часть 2. Линейный поиск с подсчётом проверок


// int linearChecks;

// int a = FindLinear(numbers, 7, out linearChecks);
// Console.WriteLine($"7: индекс {a}, проверок {linearChecks}");

// a = FindLinear(numbers, 18, out linearChecks);
// Console.WriteLine($"18: индекс {a}, проверок {linearChecks}");

// a = FindLinear(numbers, 99, out linearChecks);
// Console.WriteLine($"99: индекс {a}, проверок {linearChecks}");

// // Количество проверок зависит от положения элемента, потому что линейный поиск
// // идёт по элементам ПО ОЧЕРЕДИ и останавливается, как только находит нужный
// // Если элемент в начале — находим сразу, если в конце — проверяем
// // почти весь массив. Дольше всего поиск работает, когда
// // элемента в массиве нет вовсе или он стоит последним тогда проверяются ВСЕ
// // элементы, то есть Length проверок

// #### Ручная трассировка

// РУЧНАЯ ТРАССИРОВКА для массива [3, 7, 12, 18, 25, 31, 42, 50, 64], target = 42:
//
// | Шаг | left | right | mid | numbers[mid] | Решение          |
// |-----|------|-------|-----|--------------|------------------|
// | 1   | 0    | 8     | 4   | 25           | 42 > 25 — вправо |
// | 2   | 5    | 8     | 6   | 42           | Найдено! Индекс 6|
//
// Трассировка для target = 20 (значения в массиве нет):
//
// | Шаг | left | right | mid | numbers[mid] | Решение           |
// |-----|------|-------|-----|--------------|-------------------|
// | 1   | 0    | 8     | 4   | 25           | 20 < 25 — влево   |
// | 2   | 0    | 3     | 1   | 7            | 20 > 7 — вправо   |
// | 3   | 2    | 3     | 2   | 12           | 20 > 12 — вправо  |
// | 4   | 3    | 3     | 3   | 18           | 20 > 18 — вправо  |
// | 5   | 4    | 3     | —   | —            | left > right: конец|
//
// Цикл закончился тем, что left стал больше right — диапазон опустел,
// значение 20 в массиве отсутствует, возвращаем -1.

// // РУЧНАЯ ТРАССИРОВКА для массива [3, 7, 12, 18, 25, 31, 42, 50, 64], target = 42:
//
// | Шаг | left | right | mid | numbers[mid] | Решение          |
// |-----|------|-------|-----|--------------|------------------|
// | 1   | 0    | 8     | 4   | 25           | 42 > 25 — вправо |
// | 2   | 5    | 8     | 6   | 42           | Найдено! Индекс 6|
//
// Трассировка для target = 20 (значения в массиве нет):
//
// | Шаг | left | right | mid | numbers[mid] | Решение           |
// |-----|------|-------|-----|--------------|-------------------|
// | 1   | 0    | 8     | 4   | 25           | 20 < 25 — влево   |
// | 2   | 0    | 3     | 1   | 7            | 20 > 7 — вправо   |
// | 3   | 2    | 3     | 2   | 12           | 20 > 12 — вправо  |
// | 4   | 3    | 3     | 3   | 18           | 20 > 18 — вправо  |
// | 5   | 4    | 3     | —   | —            | left > right: конец|
//
// Цикл закончился тем, что left стал больше right — диапазон опустел,
// значение 20 в массиве отсутствует, возвращаем -1.
// РУЧНАЯ ТРАССИРОВКА для массива [3, 7, 12, 18, 25, 31, 42, 50, 64], target = 42:
//
// | Шаг | left | right | mid | numbers[mid] | Решение          |
// |-----|------|-------|-----|--------------|------------------|
// | 1   | 0    | 8     | 4   | 25           | 42 > 25 — вправо |
// | 2   | 5    | 8     | 6   | 42           | Найдено! Индекс 6|
//
// Трассировка для target = 20 (значения в массиве нет):
//
// | Шаг | left | right | mid | numbers[mid] | Решение           |
// |-----|------|-------|-----|--------------|-------------------|
// | 1   | 0    | 8     | 4   | 25           | 20 < 25 — влево   |
// | 2   | 0    | 3     | 1   | 7            | 20 > 7 — вправо   |
// | 3   | 2    | 3     | 2   | 12           | 20 > 12 — вправо  |
// | 4   | 3    | 3     | 3   | 18           | 20 > 18 — вправо  |
// | 5   | 4    | 3     | —   | —            | left > right: конец|
//
// Цикл закончился тем, что left стал больше right — диапазон опустел,
// значение 20 в массиве отсутствует, возвращаем -1.

// // РУЧНАЯ ТРАССИРОВКА для массива [3, 7, 12, 18, 25, 31, 42, 50, 64], target = 42:
//
// | Шаг | left | right | mid | numbers[mid] | Решение          |
// |-----|------|-------|-----|--------------|------------------|
// | 1   | 0    | 8     | 4   | 25           | 42 > 25 — вправо |
// | 2   | 5    | 8     | 6   | 42           | Найдено! Индекс 6|
//
// Трассировка для target = 20 (значения в массиве нет):
//
// | Шаг | left | right | mid | numbers[mid] | Решение           |
// |-----|------|-------|-----|--------------|-------------------|
// | 1   | 0    | 8     | 4   | 25           | 20 < 25 — влево   |
// | 2   | 0    | 3     | 1   | 7            | 20 > 7 — вправо   |
// | 3   | 2    | 3     | 2   | 12           | 20 > 12 — вправо  |
// | 4   | 3    | 3     | 3   | 18           | 20 > 18 — вправо  |
// | 5   | 4    | 3     | —   | —            | left > right: конец|
//
// Цикл закончился тем, что left стал больше right — диапазон опустел,
// значение 20 в массиве отсутствует, возвращаем -1

// Реализация

// int[] sortedNumbers = [3, 7, 12, 18, 25, 31, 42, 50, 64];

// int b = FindBinary(sortedNumbers, 42, out int binaryChecks);
// Console.WriteLine($"42: индекс {b}, проверок {binaryChecks}");

// b = FindBinary(sortedNumbers, 3, out binaryChecks);
// Console.WriteLine($"3: индекс {b}, проверок {binaryChecks}");

// b = FindBinary(sortedNumbers, 64, out binaryChecks);
// Console.WriteLine($"64: индекс {b}, проверок {binaryChecks}");

// b = FindBinary(sortedNumbers, 20, out binaryChecks);
// Console.WriteLine($"20: индекс {b}, проверок {binaryChecks}");

// int FindBinary(int[] numbers, int target, out int checks)
// {
//     checks = 0;

//     int left = 0;
//     int right = numbers.Length - 1;

//     while (left <= right)
//     {

//         int mid = left + (right - left) / 2;
//         checks++;

//         if (numbers[mid] == target)
//         {
//             return mid;
//         }
//         else if (numbers[mid] < target)
//         {
//             left = mid + 1;
//         }
//         else
//         {
//             right = mid - 1;
//         }
//     }

//     return -1;
// }

// ### Часть 4. Сравнение алгоритмов

// int[] data = new int[1000];

// for (int i = 0; i < data.Length; i++)
// {
//     data[i] = i * 3;
// }

// int[] targets = [0, 1497, 2997, 1];

// foreach (int target in targets)
// {
//     int linIdx = FindLinear(data, target, out int linChecks);
//     int binIdx = FindBinary(data, target, out int binChecks);

//     Console.WriteLine($"target = {target}:");
//     Console.WriteLine($"  Линейный:   индекс {linIdx}, проверок {linChecks}");
//     Console.WriteLine($"  Бинарный:   индекс {binIdx}, проверок {binChecks}");
// }

// // Алгоритмы ближе всего, когда элемент стоит в самом начале массива: линейному
// // хватает одной проверки (target = 0 → 1 проверка), бинарному нужно около 10
// // Разница максимальна, когда элемента нет или он в конце: линейный проверяет
// // все 1000 элементов, бинарному хватает примерно 10 — данные быстро растут,
// // а количество проверок бинарного поиска почти не меняется

// // Бинарный поиск может отбросить половину диапазона только потому, что массив
// // отсортирован: сравнение с серединой однозначно говорит, в какой половине
// // НАХОДИТСЯ target. Если данные не отсортированы, после сравнения с серединой
// // неизвестно, где искать значение — оно может оказаться в обеих половинах, и
// // отбрасывать половину нельзя, алгоритм теряет смысл


// ### Часть 5. Готовые методы .NET

// int[] sorted = [10, 20, 30, 40, 50];
// List<int> values = [10, 20, 30, 40];

// Console.WriteLine(Array.IndexOf(sorted, 30));
// Console.WriteLine(Array.BinarySearch(sorted, 30));

// Console.WriteLine(Array.IndexOf(sorted, 25));
// Console.WriteLine(Array.BinarySearch(sorted, 25));

// Console.WriteLine(values.IndexOf(30));
// Console.WriteLine(values.IndexOf(35));

// int[] unsorted = [50, 10, 40, 20, 30];
// Console.WriteLine(Array.BinarySearch(unsorted, 10));
// Console.WriteLine(Array.BinarySearch(unsorted, 30));
// Console.WriteLine(Array.BinarySearch(unsorted, 40));

// ## Итоговая задача. Поиск товара по артикулу

// int[] articleNumbers =
// [
//     1023, 1051, 1104, 1178, 1205,
//     1260, 1302, 1410, 1508, 1612
// ];

// Console.Write("Введите артикул: ");
// string input = Console.ReadLine() ?? "";

// if (!int.TryParse(input, out int article))
// {
//     Console.WriteLine("Это не число. Завершение работы.");
//     return;
// }

// int linIndex = FindLinear(articleNumbers, article, out int linChecks);

// Console.WriteLine();
// Console.WriteLine("Линейный поиск:");
// Console.WriteLine(linIndex >= 0 ? $"Индекс: {linIndex}" : "Товар не найден");
// Console.WriteLine($"Проверок: {linChecks}");

// int binIndex = FindBinary(articleNumbers, article, out int binChecks);

// Console.WriteLine();
// Console.WriteLine("Бинарный поиск:");
// Console.WriteLine(binIndex >= 0 ? $"Индекс: {binIndex}" : "Товар не найден");
// Console.WriteLine($"Проверок: {binChecks}");

// ДЕНИС АЛЕКСАНДРОВИЧ, Я НЕ РАЗОБРАЛАСЬ С ЭТОЙ ЗАДАЧЕЙ. 
// У МЕНЯ НЕ ПОЛУЧАЕТСЯ РЕШИТЬ ПРАВИЛЬНО. ИЗВИНИТЕ)

// ## Самостоятельные задания

// ### ★ Линейный поиск

// Console.WriteLine(ContainsValue(numbers, 25));
// Console.WriteLine(ContainsValue(numbers, 99));


// ### ★★ Бинарный поиск: последнее вхождение

// int[] duplicates = [1, 2, 2, 2, 5, 8];
// Console.WriteLine(FindLastOccurrence(duplicates, 2));
// Console.WriteLine(FindLastOccurrence(duplicates, 5));
// Console.WriteLine(FindLastOccurrence(duplicates, 7));

// ### ★★★ Выбор алгоритма

// 1. Имя среди 30 пользователей, данные не отсортированы → ЛИНЕЙНЫЙ ПОИСК:
//    данных мало, разница ничтожна, а сортировать ради одного поиска незачем
// 2. Артикул среди 1 000 000 отсортированных артикулов → БИНАРНЫЙ ПОИСК:
//    данных очень много и они уже отсортированы (до 1 000 000 проверок
//    против примерно 20)
// 3. Много раз искать в одном большом отсортированном массиве → БИНАРНЫЙ
//    ПОИСК: условие сортировки выполнено, а экономия проверок накапливается
//    с каждым запросом