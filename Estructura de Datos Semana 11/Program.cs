using System;
using System.Collections.Generic;
using System.Text.RegularExpressions; // Se usa para limpiar signos de puntuación

class Traductor
{
    // Diccionario para almacenar palabras en inglés y su traducción al español
    static Dictionary<string, string> inglesEspanol = new Dictionary<string, string>()
    {
        { "time", "tiempo" }, { "person", "persona" }, { "day", "dia" }, { "world", "mundo" },
        { "life", "vida" }, { "hand", "mano" }, { "part", "parte" }, { "eye", "ojo" },
        { "woman", "mujer" }, { "place", "lugar" }{ "water", "agua" }, { "house", "casa" }, 
        { "car", "auto" }, { "tree", "arbol" }, { "sun", "sol" } 
    };

    // Diccionario para almacenar palabras en español y su traducción al inglés
    static Dictionary<string, string> espanolIngles = new Dictionary<string, string>();

    static void Main()
    {
        // Llenar el diccionario de español a inglés con los valores del diccionario inglés-español
        foreach (var par in inglesEspanol)
        {
            espanolIngles[par.Value] = par.Key;
        }

        int opcion; // Variable para almacenar la opción del usuario

        do
        {
            // Mostrar el menú de opciones
            Console.WriteLine("\nMENU");
            Console.WriteLine("=======================================================");
            Console.WriteLine("1. Traducir una frase");
            Console.WriteLine("2. Ingresar más palabras al diccionario");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");

            // Leer la opción del usuario y verificar si es un número válido
            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine("Opción no válida. Intente de nuevo.");
                continue; // Si la opción no es válida, volver al inicio del bucle
            }

            // Ejecutar la opción seleccionada
            switch (opcion)
            {
                case 1: TraducirFrase(); break; // Llamar a la función para traducir una frase
                case 2: AgregarPalabras(); break; // Llamar a la función para agregar palabras al diccionario
                case 0: Console.WriteLine("¡Hasta luego!"); break; // Mensaje de despedida
                default: Console.WriteLine("Opción no válida."); break; // Mensaje si la opción no es válida
            }

        } while (opcion != 0); // Repetir el menú hasta que el usuario elija salir
    }

    static void TraducirFrase()
    {
        // Pedir al usuario que ingrese una frase
        Console.Write("Ingrese la frase: ");
        string frase = Console.ReadLine();

        // Dividir la frase en palabras usando los espacios como separador
        string[] palabras = frase.Split(' ');

        // Recorrer cada palabra en la frase
        for (int i = 0; i < palabras.Length; i++)
        {
            // Limpiar la palabra de signos de puntuación y convertirla a minúsculas
            string palabraLimpia = Regex.Replace(palabras[i], @"[^\w]", "").ToLower();

            // Si la palabra existe en el diccionario inglés-español, reemplazarla por su traducción
            if (inglesEspanol.ContainsKey(palabraLimpia))
            {
                palabras[i] = ConservarFormato(palabras[i], inglesEspanol[palabraLimpia]);
            }
            // Si la palabra existe en el diccionario español-inglés, reemplazarla por su traducción
            else if (espanolIngles.ContainsKey(palabraLimpia))
            {
                palabras[i] = ConservarFormato(palabras[i], espanolIngles[palabraLimpia]);
            }
        }

        // Mostrar la frase traducida uniendo las palabras modificadas
        Console.WriteLine($"Su frase traducida es: {string.Join(" ", palabras)}");
    }

    static string ConservarFormato(string original, string traducida)
    {
        // Si la palabra original empieza con mayúscula, convertir la primera letra de la traducción en mayúscula
        if (char.IsUpper(original[0]))
        {
            return char.ToUpper(traducida[0]) + traducida.Substring(1);
        }
        return traducida; // Si no, devolver la traducción en minúsculas
    }

    static void AgregarPalabras()
    {
        // Pedir al usuario una nueva palabra en inglés
        Console.Write("Ingrese la palabra en inglés: ");
        string palabraIngles = Console.ReadLine().Trim().ToLower(); // Convertir a minúsculas y eliminar espacios extra

        // Pedir al usuario su traducción en español
        Console.Write("Ingrese la traducción en español: ");
        string palabraEspanol = Console.ReadLine().Trim().ToLower(); // Convertir a minúsculas y eliminar espacios extra

        // Verificar si la palabra ya existe en el diccionario antes de agregarla
        if (!inglesEspanol.ContainsKey(palabraIngles) && !espanolIngles.ContainsKey(palabraEspanol))
        {
            // Agregar la nueva palabra al diccionario inglés-español
            inglesEspanol[palabraIngles] = palabraEspanol;
            // Agregar la nueva palabra al diccionario español-inglés
            espanolIngles[palabraEspanol] = palabraIngles;
            Console.WriteLine("Palabra añadida exitosamente."); // Confirmar que se agregó correctamente
        }
        else
        {
            Console.WriteLine("La palabra ya existe en el diccionario."); // Mensaje si la palabra ya existe
        }
    }
}

