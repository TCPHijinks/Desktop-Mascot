using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop_Mascot
{
    class MascotMovementLogic
    {
        MascotPhysics physics;
        PictureBox mascotGraphic;       
        public int moveSpeedX = 3;
        private int mascotWidth;
        private int screenWidth;
        public MascotMovementLogic(MascotPhysics physics, int mascotWidth, int screenWidth, ref PictureBox mascotGraphic)
        {
            this.physics = physics;
            this.mascotWidth = mascotWidth;
            this.screenWidth = screenWidth;
            this.mascotGraphic = mascotGraphic;
        }

        int logicTimer = 0;         // Update timer.       
        int moveSpeedCur;           // Current move speed and direction.
        int moveSpeedDefault = 1;   // Default move speed and direction.
        int rndSave = 0;            // Save results of rnd random.
        bool lockInitialDir = false;// Lock new dir when start walking.          
        public bool canWander;
        Random rnd = new Random();
        public void UpdateLogic(bool onGround, ref int mascotPosX, ref int mascotPosY)
        {
            if(canWander)
            {              
                if (onGround)
                {
                    // Check if outside boundary.
                    if ((mascotPosX + mascotWidth) >= screenWidth || mascotPosX <= 0)
                    {
                        // Move inside boundary.
                        if (mascotPosX <= 1)
                        {
                            mascotPosX = 1;
                            moveSpeedCur = moveSpeedDefault;
                        }
                        else
                        {
                            mascotPosX = (screenWidth - mascotWidth) - 1;
                            moveSpeedCur = moveSpeedDefault * -1;
                        }

                    }
                    // Otherwise select random dir.
                    else if (!lockInitialDir)
                    {
                        rndSave = rnd.Next(5);
                        if (rndSave == 1 || rndSave == 2)
                        {
                            moveSpeedCur = moveSpeedDefault * -1;
                        }
                        else
                        {
                            moveSpeedCur = moveSpeedDefault;
                        }
                        lockInitialDir = true;
                    }
                    else if (moveSpeedCur == 0)
                    {
                        moveSpeedCur = moveSpeedDefault;
                    }

                    logicTimer++;
                    if (logicTimer > 350)
                    {
                        if (rnd.Next(2000) >= 1985)
                        {
                            rndSave = rnd.Next(1, 3);

                            if (rndSave == 1)
                            {
                                if (rnd.Next(2) == 1)
                                {
                                    moveSpeedCur = moveSpeedDefault * -1;
                                }
                                else
                                {
                                    moveSpeedCur = moveSpeedDefault;
                                }
                            }
                            else if (rndSave == 2)
                            {
                                logicTimer = 0;
                                moveSpeedCur = 0;
                            }
                            else
                            {
                                moveSpeedCur = 0;
                            }
                        }
                    }

                    // Apply new movement.                  
                    if (logicTimer >= 150)
                    {
                        physics.MoveMascot(moveSpeedCur, 0, ref mascotPosX, ref mascotPosY, ref mascotGraphic);
                    }
                }
                else
                {
                    // Reset timer and allow new random start dir.
                    logicTimer = 0;
                    lockInitialDir = false;
                }
            }
        }
    }
}
