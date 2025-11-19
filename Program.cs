using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LabWork16
{
    // Клас, що відповідає за пошук зображень (Інкапсуляція логіки)
    public class ImageFinder
    {
        // Регулярний вираз для пошуку файлів із розширеннями jpg, png, gif
        // \b       - межа слова
        // \S+      - один або більше символів, що не є пробілами (URL або шлях)
        // \.       - літеральна крапка
        // (jpg|...) - група варіантів розширень
        private const string ImagePattern = @"\b\S+\.(jpg|png|gif)\b";

        /// <summary>
        /// Шукає всі посилання на зображення у переданому тексті.
        /// </summary>
        /// <param name="text">Вхідний текст для аналізу.</param>
        /// <returns>Список знайдених посилань.</returns>
        public List<string> FindImageLinks(string text)
        {
            var foundImages = new List<string>();

            if (string.IsNullOrWhiteSpace(text))
            {
                return foundImages;
            }

            // Використовуємо RegexOptions.IgnoreCase, щоб знаходити .JPG, .PnG тощо.
            MatchCollection matches = Regex.Matches(text, ImagePattern, RegexOptions.IgnoreCase);

            foreach (Match match in matches)
            {
                foundImages.Add(match.Value);
            }

            return foundImages;
        }

        /// <summary>
        /// Перевіряє, чи містить текст хоча б одне посилання на зображення.
        /// </summary>
        public bool ContainsImages(string text)
        {
            return Regex.IsMatch(text, ImagePattern, RegexOptions.IgnoreCase);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Налаштування кодування для коректного відображення кирилиці
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // 1. Створення об'єкта класу-шукача
            ImageFinder finder = new ImageFinder();

            // 2. Підготовка тестових даних (з різними варіантами)
            string inputText = "Привіт! Ось моє фото: photo.jpg. " +
                               "А це сайт: https://example.com/logo.PNG. " +
                               "Також маю файл document.pdf (його не треба), " +
                               "але є ще animation.gif та old_photo.JPG.";

            Console.WriteLine("--- Вхідний текст ---");
            Console.WriteLine(inputText);
            Console.WriteLine("---------------------");

            // 3. Виконання завдання: Перевірка наявністі
            bool hasImages = finder.ContainsImages(inputText);

            if (hasImages)
            {
                Console.WriteLine("\n[+] У тексті знайдено посилання на зображення.");
                
                // Отримання списку для деталізації
                List<string> images = finder.FindImageLinks(inputText);
                
                Console.WriteLine($"Кількість знайдених файлів: {images.Count}");
                Console.WriteLine("Список:");
                
                foreach (var img in images)
                {
                    Console.WriteLine($" -> {img}");
                }
            }
            else
            {
                Console.WriteLine("\n[-] Зображень форматів .jpg, .png, .gif не знайдено.");
            }

            // 4. Інтерактивна перевірка (введення з клавіатури)
            Console.WriteLine("\n============================================");
            Console.WriteLine("Введіть свій текст для перевірки (або натисніть Enter для виходу):");
            string userInput = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(userInput))
            {
                var userImages = finder.FindImageLinks(userInput);
                if (userImages.Count > 0)
                {
                    Console.WriteLine("Знайдено:");
                    foreach (var img in userImages) Console.WriteLine(img);
                }
                else
                {
                    Console.WriteLine("Нічого не знайдено.");
                }
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу для завершення...");
            Console.ReadKey();
        }
    }
}
