﻿using System;

namespace Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            Player player = new Player();
            board.Initialize(25, player);
            player.Initialize(1, 1, board);

            Console.CursorVisible = false;

            const int WAIT_TICK = 1000 / 30;
            int lastTick = 0;

            while (true)
            {
                #region 프레임 관리
                // FPS 프레임 (60 프레임 OK 30 프레임 이하로 NO)
                // 만약 경과한 시간이 1/30초 보다 작다면
                int currentTick = Environment.TickCount & Int32.MaxValue;
                if (currentTick - lastTick < WAIT_TICK)
                    continue;
                int deltaTick = currentTick - lastTick;
                lastTick = currentTick;
                #endregion

                // 입력

                // 로직
                player.Update(deltaTick);

                // 렌더링
                Console.SetCursorPosition(0, 0);

                board.Render();
            }
        }
    }
}
