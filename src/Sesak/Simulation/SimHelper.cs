using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sesak.Simulation
{
    public class SimHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetMagnitude(double x1, double y1, double x2, double y2)
        {
            double dx = x2 - x1;
            double dy = y2 - y1;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetMagnitude(Vector2D a, Vector2D b)
        {
            double dx = b.X - a.X;
            double dy = b.Y - a.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
        public static OldAgent[] LoadAgentsFromCSV(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);

            List<OldAgent> agents = new List<OldAgent>();
            int tagNumber = 0;
            foreach (string line in lines)
            {

                string[] parts = line.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                OldAgent a = new OldAgent();

                a.StartPos.X = double.Parse(parts[0]);
                a.StartPos.Y = double.Parse(parts[1]);

                a.EndPos.X = double.Parse(parts[2]);
                a.EndPos.Y = double.Parse(parts[3]);

                a.Vi = double.Parse(parts[4]);

                a.Vo.X = double.Parse(parts[5]);
                a.Vo.Y = double.Parse(parts[6]);

                a.Mass = double.Parse(parts[7]);
                a.BodyRad = double.Parse(parts[8]);

                a.MinRange = double.Parse(parts[9]);

                a.IndexTag = tagNumber++;

                agents.Add(a);
            }

            return agents.ToArray();
        }
        public static OldObstacle[] LoadObstaclesFromCSV(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);

            List<OldObstacle> obstacles = new List<OldObstacle>();

            foreach (string line in lines)
            {
                string[] parts = line.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                OldObstacle a = new OldObstacle();

                a.StartPos.X = double.Parse(parts[0]);
                a.StartPos.Y = double.Parse(parts[1]);

                a.EndPos.X = double.Parse(parts[2]);
                a.EndPos.Y = double.Parse(parts[3]);

                obstacles.Add(a);
            }

            return obstacles.ToArray();
        }


        #region FijAgent
        public static Vector2D FijAgent(double A, double B, double k, double kappa, Vector2D P1, Vector2D P2, double bodyRadiusI, double bodyRadiusJ, Vector2D currentVel_n, Vector2D currentVel_p) //function[Fijout] = Fijagent(A, B, k, kappa, P1, P2, BodyRadiusI, BodyRadiusJ, CurrentVel_n, CurrentVel_p);
        {
            Vector2D vDiff = new Vector2D(currentVel_p.X - currentVel_n.X, currentVel_p.Y - currentVel_n.Y);

            Vector2D pDiff = new Vector2D(P1.X - P2.X, P1.Y - P2.Y);

            double Dij = pDiff.GetMagnitude();

            double rij = bodyRadiusI + bodyRadiusJ;

            //% check distance condition %

            if (Dij > rij)
            {
                //calculate Fij only

                Vector2D PNorm = pDiff.Normalize();

                //% ----calculate repulsion force between agent, i &j---- %

                //% calculate socio - psychological force, Fij ^ s %

                double C = (rij - Dij) / B;

                double Fij = A * Math.Exp(C);

                Vector2D Fij_out = new Vector2D(PNorm.X * Fij, PNorm.Y * Fij);

                return Fij_out;

            }
            else
            {


                Vector2D PNorm = pDiff.Normalize();
                Vector2D tangentDir = new Vector2D(-PNorm.Y, PNorm.X);

                Vector2D tangentVel = new Vector2D(vDiff.X * tangentDir.X, vDiff.Y * tangentDir.Y);


                //% ----calculate repulsion force between agent, i &j---- %

                //% calculate socio - psychological force, Fij ^ s %

                double C = (rij - Dij) / B;

                double Fij = A * Math.Exp(C);

                Vector2D Fij_out = new Vector2D(PNorm.X * Fij, PNorm.Y * Fij);

                //% ----calculate body compression ----%

                //% calculate physical force, Fij^ p1 %

                double Fbc = k * (rij - Dij);

                Vector2D Fbcout = new Vector2D(PNorm.X * Fbc, PNorm.Y * Fbc);

                //% ----calculate sliding friction force---- %

                //% calculate physical force, Fij^ p2 %

                double Fsf = kappa * (rij - Dij);

                Vector2D Fsfout = new Vector2D(Fsf * tangentVel.X * tangentDir.X, Fsf * tangentVel.Y * tangentDir.Y);

                //% ----Summation of socio and physical forces---- %

                return new Vector2D(Fij_out.X + Fbcout.X + Fsfout.X, Fij_out.Y + Fbcout.Y + Fsfout.Y);
            }
        }



        public static void FijAgent_opt(ref Vector2D FijRef, double A, double B, double k, double kappa, Vector2D P1, Vector2D P2, double bodyRadiusI, double bodyRadiusJ, Vector2D currentVel_n, Vector2D currentVel_p) //function[Fijout] = Fijagent(A, B, k, kappa, P1, P2, BodyRadiusI, BodyRadiusJ, CurrentVel_n, CurrentVel_p);
        {

            double vDiffX = currentVel_p.X - currentVel_n.X;
            double vDiffY = currentVel_p.Y - currentVel_n.Y;


            double pDiffX = P1.X - P2.X;
            double pDiffY = P1.Y - P2.Y;


            double Dij = Math.Sqrt(pDiffX * pDiffX + pDiffY * pDiffY);

            double rij = bodyRadiusI + bodyRadiusJ;

            //% check distance condition %
            double rijDij = rij - Dij;

            double PNormX = pDiffX / Dij;
            double PNormY = pDiffY / Dij;
            //Vector2D result = new Vector2D();

            double C = rijDij / B;

            double Fij = A * Math.Exp(C);


            FijRef.X += PNormX * Fij;
            FijRef.Y += PNormY * Fij;

            if (Dij <= rij)
            {

                //Vector2D tangentDir = new Vector2D(-PNormY, PNormX);

                //Vector2D tangentVel = new Vector2D(vDiffX * tangentDir.X, vDiffY * tangentDir.Y);
                double tangentVelX = vDiffX * -PNormY;
                double tangentVelY = vDiffY * PNormX;

                //% ----calculate repulsion force between agent, i &j---- %

                //% calculate socio - psychological force, Fij ^ s %


                //% ----calculate body compression ----%

                //% calculate physical force, Fij^ p1 %

                double Fbc = k * rijDij;

                FijRef.X += PNormX * Fbc;
                FijRef.Y += PNormY * Fbc;


                //% ----calculate sliding friction force---- %

                //% calculate physical force, Fij^ p2 %

                double Fsf = kappa * rijDij;

                //% ----Summation of socio and physical forces---- %
                FijRef.X += Fsf * tangentVelX * -PNormY;
                FijRef.Y += Fsf * tangentVelY * PNormX;

            }
        }
        #endregion

        #region Fiwobs

        public static Vector2D Fiwobs(double A, double B, double k, double kappa, Vector2D agentPosition, OldObstacle obstacle, double bodyRadiusI, double minRange, Vector2D currentVel_n) // function[Fiwout] = Fiwobs(A, B, k, kappa, agentPosition, obstacleP1, obstacleP2, BodyRadiusI, Minrange, CurrentVel_n);
        {
            //return new Vector2D();
            //% ---- for horizontal & vertical obstacle---- %
            if (obstacle.ObstacleType == ObstacleTypes.Vertical)
            {

                Vector2D dv;
                double diw;
                if (agentPosition.Y <= obstacle.StartPos.Y)
                {
                    dv = new Vector2D(agentPosition.X - obstacle.StartPos.X, agentPosition.Y - obstacle.StartPos.Y);

                    diw = dv.GetMagnitude();
                }
                else if (agentPosition.Y >= obstacle.EndPos.Y)
                {
                    dv = new Vector2D(agentPosition.X - obstacle.StartPos.X, agentPosition.Y - obstacle.EndPos.Y);

                    diw = dv.GetMagnitude();
                }
                else
                {
                    dv = new Vector2D(agentPosition.X - obstacle.StartPos.X, 0);


                    diw = Math.Abs(dv.X);

                }


                Vector2D niw = dv.Normalize();
                double C = (bodyRadiusI - diw) / B;
                double Fiw = A * Math.Exp(C);

                Vector2D Fiw_out = new Vector2D(niw.X * Fiw, niw.Y * Fiw);

                if (diw > bodyRadiusI)
                {

                    return Fiw_out;
                }
                else
                {

                    Vector2D tiw = new Vector2D(-niw.Y, niw.X);

                    //% ----calculate repulsion force ----%

                    double Fiw_bc = k * (bodyRadiusI - diw);
                    Vector2D Fiw_bcout = new Vector2D(Fiw_bc * niw.X, Fiw_bc * niw.Y);

                    double Fiw_sf = kappa * (bodyRadiusI - diw);
                    Vector2D tangVel = new Vector2D(currentVel_n.X * tiw.X, currentVel_n.Y * tiw.Y);
                    Vector2D Fiw_sfout = new Vector2D(Fiw_sf * tangVel.X * tiw.X, Fiw_sf * tangVel.Y * tiw.Y);

                    //% summation of Fiw %
                    return new Vector2D(Fiw_out.X - Fiw_sfout.X + Fiw_bcout.X, Fiw_out.Y - Fiw_sfout.Y + Fiw_bcout.Y);
                }


            }
            else if (obstacle.ObstacleType == ObstacleTypes.Horizontal)
            {

                Vector2D dv;
                double diw;
                if (agentPosition.X <= obstacle.StartPos.X)
                {

                    dv = new Vector2D(agentPosition.X - obstacle.StartPos.X, agentPosition.Y - obstacle.StartPos.Y);
                    diw = dv.GetMagnitude();

                }
                else if (agentPosition.X > obstacle.EndPos.X)
                {
                    dv = new Vector2D(agentPosition.X - obstacle.EndPos.X, agentPosition.Y - obstacle.EndPos.Y);

                    diw = dv.GetMagnitude();
                }
                else
                {
                    dv = new Vector2D(0, agentPosition.Y - obstacle.EndPos.Y);
                    diw = Math.Abs(dv.Y);

                }


                Vector2D niw = dv.Normalize();
                double C = (bodyRadiusI - diw) / B;
                double Fiw = A * Math.Exp(C);
                Vector2D Fiw_out = new Vector2D(Fiw * niw.X, Fiw * niw.Y);

                if (diw > bodyRadiusI)
                {
                    return Fiw_out;

                }

                Vector2D tiw = new Vector2D(-niw.Y, niw.X);

                //% ----calculate repulsion force ----%

                double Fiw_bc = k * (bodyRadiusI - diw);
                Vector2D Fiw_bcout = new Vector2D(Fiw_bc * niw.X, Fiw_bc * niw.Y);

                double Fiw_sf = kappa * (bodyRadiusI - diw);

                Vector2D tangVel = new Vector2D(currentVel_n.X * tiw.X, currentVel_n.Y * tiw.Y);

                Vector2D Fiw_sfout = new Vector2D(Fiw_sf * tangVel.X * tiw.X, Fiw_sf * tangVel.Y * tiw.Y);


                //% summation of Fiw %
                return new Vector2D(Fiw_out.X - Fiw_sfout.X + Fiw_bcout.X, Fiw_out.Y - Fiw_sfout.Y + Fiw_bcout.Y);

            }

            return new Vector2D();

        }

        public static Vector2D Fiwobs_opt(double A, double B, double k, double kappa, Vector2D agentPosition, ObstacleStruct obstacle, double bodyRadiusI, double minRange, Vector2D currentVel_n) // function[Fiwout] = Fiwobs(A, B, k, kappa, agentPosition, obstacleP1, obstacleP2, BodyRadiusI, Minrange, CurrentVel_n);
        {
            //return new Vector2D();
            //% ---- for horizontal & vertical obstacle---- %
            if (obstacle.obType == (byte)ObstacleTypes.Vertical)
            {

                Vector2D dv;
                double diw;
                if (agentPosition.Y <= obstacle.StartPos.Y)
                {
                    dv = new Vector2D(agentPosition.X - obstacle.StartPos.X, agentPosition.Y - obstacle.StartPos.Y);

                    diw = dv.GetMagnitude();
                }
                else if (agentPosition.Y >= obstacle.EndPos.Y)
                {
                    dv = new Vector2D(agentPosition.X - obstacle.StartPos.X, agentPosition.Y - obstacle.EndPos.Y);

                    diw = dv.GetMagnitude();
                }
                else
                {
                    dv = new Vector2D(agentPosition.X - obstacle.StartPos.X, 0);


                    diw = Math.Abs(dv.X);

                }


                Vector2D niw = dv.Normalize();
                double C = (bodyRadiusI - diw) / B;
                double Fiw = A * Math.Exp(C);

                Vector2D Fiw_out = new Vector2D(niw.X * Fiw, niw.Y * Fiw);

                if (diw > bodyRadiusI)
                {

                    return Fiw_out;
                }
                else
                {

                    Vector2D tiw = new Vector2D(-niw.Y, niw.X);

                    //% ----calculate repulsion force ----%

                    double Fiw_bc = k * (bodyRadiusI - diw);
                    Vector2D Fiw_bcout = new Vector2D(Fiw_bc * niw.X, Fiw_bc * niw.Y);

                    double Fiw_sf = kappa * (bodyRadiusI - diw);
                    Vector2D tangVel = new Vector2D(currentVel_n.X * tiw.X, currentVel_n.Y * tiw.Y);
                    Vector2D Fiw_sfout = new Vector2D(Fiw_sf * tangVel.X * tiw.X, Fiw_sf * tangVel.Y * tiw.Y);
                    
                    //% summation of Fiw %
                    return new Vector2D(Fiw_out.X - Fiw_sfout.X + Fiw_bcout.X, Fiw_out.Y - Fiw_sfout.Y + Fiw_bcout.Y);
                }


            }
            else if (obstacle.obType == (byte)ObstacleTypes.Horizontal)
            {

                Vector2D dv;
                double diw;
                if (agentPosition.X <= obstacle.StartPos.X)
                {

                    dv = new Vector2D(agentPosition.X - obstacle.StartPos.X, agentPosition.Y - obstacle.StartPos.Y);
                    diw = dv.GetMagnitude();

                }
                else if (agentPosition.X > obstacle.EndPos.X)
                {
                    dv = new Vector2D(agentPosition.X - obstacle.EndPos.X, agentPosition.Y - obstacle.EndPos.Y);

                    diw = dv.GetMagnitude();
                }
                else
                {
                    dv = new Vector2D(0, agentPosition.Y - obstacle.EndPos.Y);
                    diw = Math.Abs(dv.Y);

                }


                Vector2D niw = dv.Normalize();
                double C = (bodyRadiusI - diw) / B;
                double Fiw = A * Math.Exp(C);
                Vector2D Fiw_out = new Vector2D(Fiw * niw.X, Fiw * niw.Y);

                if (diw > bodyRadiusI)
                {
                    return Fiw_out;

                }

                Vector2D tiw = new Vector2D(-niw.Y, niw.X);

                //% ----calculate repulsion force ----%

                double Fiw_bc = k * (bodyRadiusI - diw);
                Vector2D Fiw_bcout = new Vector2D(Fiw_bc * niw.X, Fiw_bc * niw.Y);

                double Fiw_sf = kappa * (bodyRadiusI - diw);

                Vector2D tangVel = new Vector2D(currentVel_n.X * tiw.X, currentVel_n.Y * tiw.Y);

                Vector2D Fiw_sfout = new Vector2D(Fiw_sf * tangVel.X * tiw.X, Fiw_sf * tangVel.Y * tiw.Y);


                //% summation of Fiw %
                return new Vector2D(Fiw_out.X - Fiw_sfout.X + Fiw_bcout.X, Fiw_out.Y - Fiw_sfout.Y + Fiw_bcout.Y);



            }

            return new Vector2D();

        }

    }
    #endregion
}
