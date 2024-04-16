

class Program
{
    static void Main()
    {
        const int tamañoTablero = 10;
        char[,] tableroJugador1 = new char[tamañoTablero, tamañoTablero];
        char[,] tableroJugador2 = new char[tamañoTablero, tamañoTablero];

        InicializarTablero(tableroJugador1);
        InicializarTablero(tableroJugador2);

        Console.WriteLine("¡Bienvenidos al juego de Batalla Naval!");

        Console.WriteLine("Jugador 1, coloca tus barcos:");
        ColocarBarcos(tableroJugador1);

        Console.Clear(); 

        Console.WriteLine("Jugador 2, coloca tus barcos:");
        ColocarBarcos(tableroJugador2);

        Console.Clear(); 

        Console.WriteLine("¡Comienza la batalla naval!");

        while (true)
        {
            Console.WriteLine("Turno del Jugador 1:");
            RealizarDisparo(tableroJugador2);
            if (TodosLosBarcosHundidos(tableroJugador2))
            {
                Console.WriteLine("¡Felicidades, Jugador 1! ¡Has ganado!");
                break;
            }

            Console.WriteLine("Turno del Jugador 2:");
            RealizarDisparo(tableroJugador1);
            if (TodosLosBarcosHundidos(tableroJugador1))
            {
                Console.WriteLine("¡Felicidades, Jugador 2! ¡Has ganado!");
                break;
            }
        }

        Console.WriteLine("¡Gracias por jugar!");
        Console.ReadLine();
    }

    static void InicializarTablero(char[,] tablero)
    {
        for (int i = 0; i < tablero.GetLength(0); i++)
        {
            for (int j = 0; j < tablero.GetLength(1); j++)
            {
                tablero[i, j] = ' ';
            }
        }
    }

    static void ImprimirTablero(char[,] tablero)
    {
        Console.WriteLine("   A B C D E F G H I J");
        for (int i = 0; i < tablero.GetLength(0); i++)
        {
            Console.Write($"{i + 1,2} ");
            for (int j = 0; j < tablero.GetLength(1); j++)
            {
                Console.Write(tablero[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    static void ColocarBarcos(char[,] tablero)
    {
        ImprimirTablero(tablero);
        Console.WriteLine("Coloca tus barcos en el tablero.");
        Console.WriteLine("Ingresa las coordenadas de inicio y fin para cada barco (por ejemplo, A1 A3):");

       
        string[] nombresBarcos = { "Portaaviones (5)", "Acorazado (4)", "Crucero (3)", "Submarino (3)", "Destructor (2)" };
        int[] tamañosBarcos = { 5, 4, 3, 3, 2 };

        for (int i = 0; i < nombresBarcos.Length; i++)
        {
            Console.WriteLine($"Colocando {nombresBarcos[i]}");
            Console.Write("Coordenada de inicio: ");
            string inicio = Console.ReadLine().Trim().ToUpper();
            Console.Write("Coordenada de fin: ");
            string fin = Console.ReadLine().Trim().ToUpper();

            // Convertir las coordenadas a índices de matriz
            int filaInicio = inicio[1] - '1';
            int columnaInicio = inicio[0] - 'A';
            int filaFin = fin[1] - '1';
            int columnaFin = fin[0] - 'A';

            // Validar si las coordenadas son válidas y si el barco cabe en la ubicación
            if (filaInicio < 0 || filaInicio >= tablero.GetLength(0) || filaFin < 0 || filaFin >= tablero.GetLength(0) ||
                columnaInicio < 0 || columnaInicio >= tablero.GetLength(1) || columnaFin < 0 || columnaFin >= tablero.GetLength(1) ||
                (filaInicio != filaFin && columnaInicio != columnaFin) || // Barco no está en línea recta
                (filaInicio != filaFin && Math.Abs(filaInicio - filaFin) + 1 != tamañosBarcos[i]) || // Barco no tiene la longitud correcta
                (columnaInicio != columnaFin && Math.Abs(columnaInicio - columnaFin) + 1 != tamañosBarcos[i]) || // Barco no tiene la longitud correcta
                (filaInicio == filaFin && Math.Abs(columnaInicio - columnaFin) + 1 != tamañosBarcos[i]) || // Barco no tiene la longitud correcta
                (columnaInicio == columnaFin && Math.Abs(filaInicio - filaFin) + 1 != tamañosBarcos[i]) || // Barco no tiene la longitud correcta
                HayBarcoEnRango(tablero, filaInicio, columnaInicio, filaFin, columnaFin))
            {
                Console.WriteLine("Coordenadas inválidas o barco no se puede colocar en esa posición. Intenta de nuevo.");
                i--;
                continue;
            }

           
            if (filaInicio == filaFin) 
            {
                for (int j = columnaInicio; j <= columnaFin; j++)
                {
                    tablero[filaInicio, j] = 'B'; 
                }
            }
            else 
            {
                for (int j = filaInicio; j <= filaFin; j++)
                {
                    tablero[j, columnaInicio] = 'B'; 
                }
            }

            ImprimirTablero(tablero);
        }
    }

    static bool HayBarcoEnRango(char[,] tablero, int filaInicio, int columnaInicio, int filaFin, int columnaFin)
    {
        for (int i = filaInicio; i <= filaFin; i++)
        {
            for (int j = columnaInicio; j <= columnaFin; j++)
            {
                if (tablero[i, j] == 'B') 
                {
                    return true;
                }
            }
        }
        return false;
    }

    static void RealizarDisparo(char[,] tablero)
    {
        ImprimirTablero(tablero);
        Console.WriteLine("Ingresa la coordenada para disparar (por ejemplo, A5):");
        string input = Console.ReadLine().Trim().ToUpper();

        
        int columna = input[0] - 'A';
        int fila = int.Parse(input.Substring(1)) - 1;

     
        if (fila < 0 || fila >= tablero.GetLength(0) || columna < 0 || columna >= tablero.GetLength(1))
        {
            Console.WriteLine("Coordenada inválida. Intenta de nuevo.");
            RealizarDisparo(tablero);
            return;
        }

       
        if (tablero[fila, columna] == 'B')
        {
            Console.WriteLine("¡Impacto! Has alcanzado un barco enemigo.");
            tablero[fila, columna] = 'X'; 
        }
        else if (tablero[fila, columna] == 'X' || tablero[fila, columna] == 'O')
        {
            Console.WriteLine("Ya has disparado en esta coordenada. Intenta de nuevo.");
            RealizarDisparo(tablero);
        }
        else
        {
            Console.WriteLine("¡Agua! No has alcanzado ningún barco.");
            tablero[fila, columna] = 'O'; 
        }
    }

    static bool TodosLosBarcosHundidos(char[,] tablero)
    {
        
        int barcosRestantes = 0;
        for (int i = 0; i < tablero.GetLength(0); i++)
        {
            for (int j = 0; j < tablero.GetLength(1); j++)
            {
                if (tablero[i, j] == 'B') 
                {
                    barcosRestantes++;
                }
            }
        }
        return barcosRestantes == 0; 
    }
}
