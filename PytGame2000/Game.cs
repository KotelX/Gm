using PytGame2000;
using System;
namespace PytGame2000
{
    static class Game
    {
        public static bool GameSetings(int size)
        {
            if (size < 2 || size >= 10)
                return false;
            Size = size;
            Map = new int[Size,Size];
            SpacePosition.SetNewSpacePosition(new Position { X = size - 1, Y = size - 1 });
            return true;
        }
        public static int Size { get; private set; } = 2;        //размер поля
        public static int[,] Map;                           //масив со значениями кнопок
        public static int GetValueInMap(Position position)
        {
            return Map[position.X, position.Y];
        }
        public static bool SetValueInMap(int value, Position position)
        {
            Map[position.X, position.Y] = value;
            return true;
        }
        public static bool Shift(Position position)
        {
            if ((Math.Abs(SpacePosition.Position.X - position.X) + Math.Abs(SpacePosition.Position.Y - position.Y) >= 2) || (position.X >= Size || position.Y >= Size) || position.X < 0 || position.Y < 0)
                return false;
            //Map[SpacePosition.X, SpacePosition.Y] = Map[position.X, position.Y];
            SetValueInMap(GetValueInMap(position), SpacePosition.Position);
            Map[position.X, position.Y] = 0;
            SpacePosition.SetNewSpacePosition(position);
            return true;
        }     //обработка нажатий
        public static bool GenerateReandomMap(int cycle)
        {
            var random = new Random();
            for (int i = 0; i < cycle; i++)
            {
                int randomNumber = random.Next(0, 4);
                switch (randomNumber)
                {
                    case 0:
                        if (Shift(new Position { X = SpacePosition.Position.X, Y = SpacePosition.Position.Y + 1 })) randomNumber++;      //вправо
                        break;
                    case 1:
                        if (Shift(new Position { X = SpacePosition.Position.X + 1, Y = SpacePosition.Position.Y })) randomNumber++;      //вниз
                        break;
                    case 2:
                        if (Shift(new Position {X =  SpacePosition.Position.X, Y = SpacePosition.Position.Y - 1 })) randomNumber++;      //влево
                        break;
                    case 3:
                        if (Shift(new Position { X = SpacePosition.Position.X - 1, Y = SpacePosition.Position.Y })) randomNumber--;      //вверх
                        break;
                }
            }
            return true;
        }   //генерация рандомного поля
    }
}
