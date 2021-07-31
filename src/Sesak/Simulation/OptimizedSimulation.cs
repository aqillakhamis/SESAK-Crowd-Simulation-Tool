using Sesak.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesak.Simulation
{
    public class OptimizedSimulation
    {

        /*
        public static Action<int> OldAgentLoopV(List<OldAgent> ActiveOldAgent, Obstacle[] obstacles, ZoneData[] Zones, double[] simParam)
        {
            return i =>
            {
                if (agents[i].OldAgentReachDestination == 1)
                {
                    return;
                }

                //Check in zone
                int zoneIndex = -1;

                for (int j = 0; j < zones.Length; j++)
                {
                    if (agents[i].PoX >= zones[j].TopLeftX && agents[i].PoX <= zones[j].BottomRightX && agents[i].PoY >= zones[j].BottomRightY && agents[i].PoY <= zones[j].TopLeftY)
                    {
                        zoneIndex = j;
                        break;
                    }
                }

                if (zoneIndex < 0)
                {
                    agents[i].OldAgentReachDestinationNext = 1;
                    return;
                }

                //if (result[i].CurrentZoneIndex != zoneIndex)
                {
                    agents[i].CurrentZoneIndex = zoneIndex;
                    if (zones[zoneIndex].ExitZoneIndex == -1)
                    {
                        agents[i].TargetPosX = (zones[zoneIndex].TopLeftX + zones[zoneIndex].BottomRightX) / 2;
                        agents[i].TargetPosY = (zones[zoneIndex].TopLeftY + zones[zoneIndex].BottomRightY) / 2;
                    }
                    else
                    {
                        agents[i].TargetPosX = zones[zoneIndex].DoorCenterPositionX;
                        agents[i].TargetPosY = zones[zoneIndex].DoorCenterPositionY;
                    }
                }



                double distanceDeltaX = agents[i].TargetPosX - agents[i].PoX;
                double distanceDeltaY = agents[i].TargetPosY - agents[i].PoY;

                double distance = Math.Sqrt(distanceDeltaX * distanceDeltaX + distanceDeltaY * distanceDeltaY);

                //% ----OldAgent Motivational to Move towards Target Destination, Fo ----%

                //Vector2D Fo = new Vector2D();
                double FoX = 0;
                double FoY = 0;

                if (distance > 0)
                {
                    //% calculate motivational force, Fo %

                    double targetDirX = distanceDeltaX / distance;
                    double targetDirY = distanceDeltaY / distance;

                    FoX = targetDirX * agents[i].Vi;
                    FoY = targetDirY * agents[i].Vi;

                    FoX -= agents[i].VoX;
                    FoY -= agents[i].VoY;

                    double tmpmul = agents[i].Mass / simParam[Delta];
                    FoX *= tmpmul;
                    FoY *= tmpmul;
                }


                if (distance <= simParam[ReachDestinationRange] && zones[zoneIndex].ExitZoneIndex == -1)
                {
                    agents[i].OldAgentReachDestinationNext = 1;
                    return;
                }



                //% ----OldAgent Repulsion Force with Other OldAgent---- %


                double FijX = 0;
                double FijY = 0;
                
                for (int j = 0; j < agents.Length; j++)
                {
                    if (i == j)
                        continue;

                    if (agents[j].OldAgentReachDestination == 1)
                        continue;

                    //SimHelper.FijOldAgent(SimParam.A,    SimParam.B, SimParam.k, SimParam.kappa, a.Po,           b.Po,           a.BodyRad,              b.BodyRad,              a.Vo,                   b.Vo);
                    // Vector2D FijOldAgent(double A,      double B,   double k,   double kappa,   Vector2D P1,    Vector2D P2,    double bodyRadiusI,     double bodyRadiusJ,     Vector2D currentVel_n,  Vector2D currentVel_p) 
                    Vector2D tmpFij = SimHelper.FijOldAgent(simParam[A], simParam[B], simParam[k], simParam[kappa], new Vector2D(agents[i].PoX, agents[i].PoY), new Vector2D(agents[j].PoX, agents[j].PoY), agents[i].BodyRad, agents[j].BodyRad, new Vector2D(agents[i].VoX, agents[i].VoY), new Vector2D(agents[j].VoX, agents[j].VoY)); //Fijagent(A, B, k, kappa, OldAgent(n).('Po'), OldAgent(p).('Po'), OldAgent(n).('BodyRad'), OldAgent(p).('BodyRad'), OldAgent(n).('Vo'), OldAgent(p).('Vo'));

                    FijX += tmpFij.X;
                    FijY += tmpFij.Y;

                    /*
                     
                    #region FijOldAgent
                    // agents[j] B
                    // agents[i] A

                    //Vector2D vDiff = new Vector2D(currentVel_p.X - currentVel_n.X, currentVel_p.Y - currentVel_n.Y);
                    double vDiffX = agents[j].VoX - agents[i].VoX;
                    double vDiffY = agents[j].VoY - agents[i].VoY;

                    //Vector2D pDiff = new Vector2D(P1.X - P2.X, P1.Y - P2.Y);
                    double pDiffX = agents[i].PoX - agents[j].PoX;
                    double pDiffY = agents[i].PoY - agents[j].PoY;

                    //double Dij = pDiff.GetMagnitude();
                    double Dij = Math.Sqrt(pDiffX * pDiffX + pDiffY * pDiffY);

                    //double rij = bodyRadiusI + bodyRadiusJ;
                    double rij = agents[i].BodyRad + agents[j].BodyRad;

                    double rijDijDiff = rij - Dij;

                    //% check distance condition %
                    double PNormX = pDiffX / Dij;
                    double PNormY = pDiffY / Dij;

                    if (Dij > rij)
                    {
                        //calculate Fij only
                        //Vector2D PNorm = pDiff.Normalize();



                        //% ----calculate repulsion force between agent, i &j---- %

                        //% calculate socio - psychological force, Fij ^ s %

                        //double C = (rij - Dij) / B;
                        double C = rijDijDiff / simParam[B];

                        //double Fij = A * Math.Exp(C);
                        double Fij = simParam[A] * Math.Exp(C);


                        //Vector2D Fij_out = new Vector2D(PNorm.X * Fij, PNorm.Y * Fij);
                        double fij_outX = PNormX * Fij;
                        double fij_outY = PNormY * Fij;
                        //return Fij_out;


                        FijX += fij_outX;
                        FijY += fij_outY;
                    }
                    else
                    {
                        
                        //Vector2D PNorm = pDiff.Normalize();
                        //Vector2D tangentDir = new Vector2D(-PNorm.Y, PNorm.X);
                        double tangentDirX = -PNormY;
                        double tangentDirY = PNormX;


                        //Vector2D tangentVel = new Vector2D(vDiff.X * tangentDir.X, vDiff.Y * tangentDir.Y);
                        double tangentVelX = vDiffX * tangentDirX;
                        double tangentVelY = vDiffY * tangentDirY;


                        //% ----calculate repulsion force between agent, i &j---- %

                        //% calculate socio - psychological force, Fij ^ s %

                        //double C = (rij - Dij) / B;
                        double C = rijDijDiff / simParam[B];

                        //double Fij = A * Math.Exp(C);
                        double Fij = simParam[A] * Math.Exp(C);

                        //Vector2D Fij_out = new Vector2D(PNorm.X * Fij, PNorm.Y * Fij);
                        //FijX += PNormX * Fij;
                        //FijY += PNormY * Fij;



                        //% ----calculate body compression ----%

                        //% calculate physical force, Fij^ p1 %

                        //double Fbc = k * (rij - Dij);
                        double Fbc = simParam[k] * rijDijDiff;

                        //Vector2D Fbcout = new Vector2D(PNorm.X * Fbc, PNorm.Y * Fbc);

                        //FijX += PNormX * Fbc;
                        //FijY += PNormY * Fbc;


                        //% ----calculate sliding friction force---- %

                        //% calculate physical force, Fij^ p2 %

                        //double Fsf = kappa * (rij - Dij);
                        double Fsf = simParam[kappa] * rijDijDiff;

                        //Vector2D Fsfout = new Vector2D(Fsf * tangentVel.X * tangentDir.X, Fsf * tangentVel.Y * tangentDir.Y);

                        //FijX += Fsf * tangentVelX * tangentDirX;
                        //FijY += Fsf * tangentVelY * tangentDirY;

                        //% ----Summation of socio and physical forces---- %

                        //return new Vector2D(Fij_out.X + Fbcout.X + Fsfout.X, Fij_out.Y + Fbcout.Y + Fsfout.Y);
                        
                    }
                    
                    #endregion


                }



                //% ----OldAgent Repulsion Force with Obstacle ----%

                //Vector2D Fiw = new Vector2D();
                double FiwX = 0;
                double FiwY = 0;
                for (int j = 0; j < obstacles.Length; j++)
                {

                    //Vector2D tmpFiw = SimHelper.Fiwobs(SimParam.A, SimParam.B, SimParam.k, SimParam.kappa, a.Po, obs, a.BodyRad, a.MinRange, a.Vo);

                    
                    // ---- for horizontal & vertical obstacle---- 
                    if (obstacles[j].obType == 1)
                    {

                        //Vector2D dv;
                        double dvX = 0;
                        double dvY = 0;
                        double diw = 1;

                        if (agents[i].PoY <= obstacles[j].StartPosY)
                        {
                            //dv = new Vector2D(agentPosition.X - obstacle.StartPos.X, agentPosition.Y - obstacle.StartPos.Y);
                            dvX = agents[i].PoX - obstacles[j].StartPosX;
                            dvY = agents[i].PoY - obstacles[j].StartPosY;

                            diw = Math.Sqrt(dvX * dvX + dvY * dvY); //dv.GetMagnitude();
                        }
                        else if (agents[i].PoY >= obstacles[j].EndPosY)
                        {
                            //dv = new Vector2D(agentPosition.X - obstacle.EndPos.X, agentPosition.Y - obstacle.EndPos.Y);
                            dvX = agents[i].PoX - obstacles[j].EndPosX;
                            dvY = agents[i].PoY - obstacles[j].EndPosY;

                            diw = Math.Sqrt(dvX * dvX + dvY * dvY);
                        }
                        else
                        {
                            dvX = agents[i].PoX - obstacles[j].StartPosX;//new Vector2D(, 0);

                            //diw = Math.Abs(dv.X);
                            if (dvX < 0)
                                diw = -dvX;
                            else
                                diw = dvX;


                        }


                        //Vector2D niw = dv.Normalize();
                        double niwX = dvX / diw;
                        double niwY = dvY / diw;

                        //double C = (bodyRadiusI - diw) / B;
                        double C = (agents[i].BodyRad - diw) / simParam[B];

                        //double Fiw = A * Math.Exp(C);
                        double Fiw = simParam[A] * Math.Exp(C);


                        //Vector2D Fiw_out = new Vector2D(niw.X * Fiw, niw.Y * Fiw);

                        FiwX += niwX * Fiw;
                        FiwY += niwY * Fiw;

                        if (diw <= agents[i].BodyRad)
                        {

                            //Vector2D tiw = new Vector2D(-niw.Y, niw.X);
                            double tiwX = -niwX;
                            double tiwY = niwX;
                            //% ----calculate repulsion force ----%

                            //double Fiw_bc = k * (bodyRadiusI - diw);
                            double Fiw_bc = simParam[k] * (agents[i].BodyRad - diw);

                            //Vector2D Fiw_bcout = new Vector2D(Fiw_bc * niw.X, Fiw_bc * niw.Y);
                            FiwX += Fiw_bc * niwX;
                            FiwY += Fiw_bc * niwY;

                            //double Fiw_sf = kappa * (bodyRadiusI - diw);
                            double Fiw_sf = simParam[kappa] * (agents[i].BodyRad - diw);


                            //Vector2D tangVel = new Vector2D(currentVel_n.X * tiw.X, currentVel_n.Y * tiw.Y);
                            double tangVelX = agents[i].VoX * tiwX;
                            double tangVelY = agents[i].VoY * tiwY;

                            //Vector2D Fiw_sfout = new Vector2D(Fiw_sf * tangVel.X * tiw.X, Fiw_sf * tangVel.Y * tiw.Y);

                            //% summation of Fiw %
                            //return new Vector2D(Fiw_out.X - Fiw_sfout.X + Fiw_bcout.X, Fiw_out.Y - Fiw_sfout.Y + Fiw_bcout.Y);
                            FiwX -= Fiw_sf * tangVelX * tiwX;
                            FiwY -= Fiw_sf * tangVelY * tiwY;
                        }


                    }
                    else if (obstacles[j].obType == 2)//ObstacleTypes.Horizontal)
                    {

                        //Vector2D dv;
                        double dvX = 0;
                        double dvY = 0;
                        double diw = 1;

                        if (agents[i].PoX <= obstacles[j].StartPosX)
                        {

                            //dv = new Vector2D(agentPosition.X - obstacle.StartPos.X, agentPosition.Y - obstacle.StartPos.Y);
                            dvX = agents[i].PoX - obstacles[j].StartPosX;
                            dvY = agents[i].PoY - obstacles[j].StartPosY;

                            diw = Math.Sqrt(dvX * dvX + dvY * dvY);//dv.GetMagnitude();

                        }
                        else if (agents[i].PoX > obstacles[j].EndPosX)
                        {
                            //dv = new Vector2D(agentPosition.X - obstacle.EndPos.X, agentPosition.Y - obstacle.EndPos.Y);
                            dvX = agents[i].PoX - obstacles[j].EndPosX;
                            dvY = agents[i].PoY - obstacles[j].EndPosY;

                            diw = Math.Sqrt(dvX * dvX + dvY * dvY);
                        }
                        else
                        {
                            //dv = new Vector2D(0, agentPosition.Y - obstacle.EndPos.Y);
                            dvY = agents[i].PoY - obstacles[j].EndPosY;
                            if (dvY < 0)
                                diw = -dvY;
                            else
                                diw = dvY;
                            //diw = Math.Abs(dv.Y);

                        }


                        //Vector2D niw = dv.Normalize();
                        double niwX = dvX / diw;
                        double niwY = dvY / diw;

                        //double C = (bodyRadiusI - diw) / B;
                        double C = (agents[i].BodyRad - diw) / simParam[B];

                        //double Fiw = A * Math.Exp(C);
                        double Fiw = simParam[A] * Math.Exp(C);


                        //Vector2D Fiw_out = new Vector2D(niw.X * Fiw, niw.Y * Fiw);

                        FiwX += niwX * Fiw;
                        FiwY += niwY * Fiw;

                        if (diw <= agents[i].BodyRad)
                        {
                            //Vector2D tiw = new Vector2D(-niw.Y, niw.X);

                            double tiwX = -niwX;
                            double tiwY = niwX;
                            //% ----calculate repulsion force ----%

                            //double Fiw_bc = k * (bodyRadiusI - diw);
                            double Fiw_bc = simParam[k] * (agents[i].BodyRad - diw);

                            //Vector2D Fiw_bcout = new Vector2D(Fiw_bc * niw.X, Fiw_bc * niw.Y);
                            FiwX += Fiw_bc * niwX;
                            FiwY += Fiw_bc * niwY;

                            //double Fiw_sf = kappa * (bodyRadiusI - diw);

                            double Fiw_sf = simParam[kappa] * (agents[i].BodyRad - diw);


                            //Vector2D tangVel = new Vector2D(currentVel_n.X * tiw.X, currentVel_n.Y * tiw.Y);
                            double tangVelX = agents[i].VoX * tiwX;
                            double tangVelY = agents[i].VoY * tiwY;

                            //Vector2D Fiw_sfout = new Vector2D(Fiw_sf * tangVel.X * tiw.X, Fiw_sf * tangVel.Y * tiw.Y);

                            //% summation of Fiw %
                            //return new Vector2D(Fiw_out.X - Fiw_sfout.X + Fiw_bcout.X, Fiw_out.Y - Fiw_sfout.Y + Fiw_bcout.Y);
                            FiwX -= Fiw_sf * tangVelX * tiwX;
                            FiwY -= Fiw_sf * tangVelY * tiwY;

                        }


                    }

                    
                }

                //FiwX = 0;
                //FiwY = 0;

                double FtotX = FoX / agents[i].Mass + FijX / agents[i].Mass + FiwX / agents[i].Mass;
                double FtotY = FoY / agents[i].Mass + FijY / agents[i].Mass + FiwY / agents[i].Mass;

                //Vector2D Acc = new Vector2D(Ftot.X, Ftot.Y);




                //a.NextVel.X = a.Vo.X + Acc.X * SimParam.dt;
                //a.NextVel.Y = a.Vo.Y + Acc.Y * SimParam.dt;
                agents[i].NextVelX = agents[i].VoX + FtotX * simParam[dt];
                agents[i].NextVelY = agents[i].VoY + FtotY * simParam[dt];


                double nextVelMag = Math.Sqrt(agents[i].NextVelX * agents[i].NextVelX + agents[i].NextVelY * agents[i].NextVelY);//a.NextVel.GetMagnitude();

                if (nextVelMag > agents[i].Vi)
                {

                    double nextValMul = agents[i].Vi / nextVelMag;
                    //a.NextVel.X *= nextValMul;
                    //a.NextVel.Y *= nextValMul;
                    agents[i].NextVelX *= nextValMul;
                    agents[i].NextVelY *= nextValMul;
                }




                //a.NextPos.X = a.Po.X + a.NextVel.X * SimParam.dt;
                //a.NextPos.Y = a.Po.Y + a.NextVel.Y * SimParam.dt;
                agents[i].NextPosX = agents[i].PoX + agents[i].NextVelX * simParam[dt];
                agents[i].NextPosY = agents[i].PoY + agents[i].NextVelY * simParam[dt];

                if (i == Validator.OldAgentMark)
                {
                    Validator.SetValueB(agents[i].NextPosX, CurrentTick);
                }

                //rounding PO to avoid floating point error in x86 vs x64
                agents[i].NextPosX = Math.Round(agents[i].NextPosX, Vector2D.ROUNDINGDECIMAL);
                agents[i].NextPosY = Math.Round(agents[i].NextPosY, Vector2D.ROUNDINGDECIMAL);



                return;
            };
        }

        */
        public static Action<int> AgentLoop(OldAgent[] agents,Line[] obstacleLines, ObstacleStruct[] ObstaclesHorizontal, ObstacleStruct[] ObstaclesVertical, SimulationParameters SimParam)
        {
            return itt =>
            {
                OldAgent a = agents[itt];

                if (a.ReachDestination)
                    return;

                //NEW WAYPOINT SYSTEM
                /*
                //Check in zone
                ZoneData currentZone = null;
                for (int i = 0; i < Zones.Length; i++)
                {
                    if (Zones[i].IsInZone(a.Po))
                    {
                        //in zone
                        a.TargetPos = Zones[i].GetExitTargetPosition(a.Po);
                        currentZone = Zones[i];
                        if (currentZone.IsCorridor)
                            a.startMeasureSpeed = true;

                        break;
                    }
                }

                if (currentZone == null)
                {
                    a.Next_ReachDestination = true;
                    continue;
                }
                */
                a.CurrentWaypointIndex = WaypointHelper.GetNextTarget(a.Waypoint, obstacleLines, a.Po, a.CurrentWaypointIndex);
                if (a.CurrentWaypointIndex >= 0)
                    a.TargetPos = a.Waypoint[a.CurrentWaypointIndex];
                else
                    a.TargetPos = a.Waypoint[a.Waypoint.Length - 1];

                //% ----Agent Motivational to Move towards Target Destination, Fo ----%

                double distance = SimHelper.GetMagnitude(a.Po, a.TargetPos);


                Vector2D Fo = new Vector2D();



                if (distance > 0)
                {

                    Vector2D tmp = new Vector2D(a.TargetPos.X - a.Po.X, a.TargetPos.Y - a.Po.Y);


                    //% calculate motivational force, Fo %

                    double tmpMagnitude = tmp.GetMagnitude();
                    Vector2D targetdir = new Vector2D(tmp.X / tmpMagnitude, tmp.Y / tmpMagnitude);
                    Vector2D vt = new Vector2D(targetdir.X * a.Vi, targetdir.Y * a.Vi);

                    vt.X -= a.Vo.X;
                    vt.Y -= a.Vo.Y;


                    double tmpmul = a.Mass / SimParam.Delta;
                    Fo.X = (vt.X * tmpmul);
                    Fo.Y = (vt.Y * tmpmul);
                }


                if (distance <= SimParam.ReachDestinationRange && a.CurrentWaypointIndex == a.Waypoint.Length - 1)// && currentZone != null && currentZone.ExitZoneIndex == -1)
                {
                    a.Next_ReachDestination = true;
                    return;
                }

                //% ----Agent Repulsion Force with Other Agent---- %

                Vector2D Fij = new Vector2D();

                foreach (OldAgent b in agents)
                {
                    if (b == a)
                        continue;


                    if (b.ReachDestination)
                        continue;

                    Vector2D tmpFij = SimHelper.FijAgent(SimParam.A, SimParam.B, SimParam.k, SimParam.kappa, a.Po, b.Po, a.BodyRad, b.BodyRad, a.Vo, b.Vo); //Fijagent(A, B, k, kappa, Agent(n).('Po'), Agent(p).('Po'), Agent(n).('BodyRad'), Agent(p).('BodyRad'), Agent(n).('Vo'), Agent(p).('Vo'));

                    Fij.X += tmpFij.X;
                    Fij.Y += tmpFij.Y;
                    //Console.WriteLine("X:" + Fij.X +", Y:"+ Fij.Y);


                }


                //% ----Agent Repulsion Force with Obstacle ----%

                Vector2D Fiw = new Vector2D();
                foreach (ObstacleStruct obstacle in ObstaclesHorizontal)
                {

                    //Vector2D tmpFiw = SimHelper.Fiwobs(SimParam.A, SimParam.B, SimParam.k, SimParam.kappa, a.Po, obs, a.BodyRad, a.MinRange, a.Vo);
                    Vector2D tmpFiw;
                    {

                        Vector2D dv;
                        double diw;
                        if (a.Po.X <= obstacle.StartPos.X)
                        {

                            dv = new Vector2D(a.Po.X - obstacle.StartPos.X, a.Po.Y - obstacle.StartPos.Y);
                            diw = dv.GetMagnitude();

                        }
                        else if (a.Po.X > obstacle.EndPos.X)
                        {
                            dv = new Vector2D(a.Po.X - obstacle.EndPos.X, a.Po.Y - obstacle.EndPos.Y);

                            diw = dv.GetMagnitude();
                        }
                        else
                        {
                            dv = new Vector2D(0, a.Po.Y - obstacle.EndPos.Y);
                            diw = Math.Abs(dv.Y);

                        }


                        Vector2D niw = dv.Normalize();
                        double C = (a.BodyRad - diw) / SimParam.B;
                        double tFiw = SimParam.A * Math.Exp(C);
                        Vector2D Fiw_out = new Vector2D(tFiw * niw.X, tFiw * niw.Y);

                        if (diw > a.BodyRad)
                        {
                            tmpFiw = Fiw_out;

                        }
                        else
                        {
                            Vector2D tiw = new Vector2D(-niw.Y, niw.X);

                            //% ----calculate repulsion force ----%

                            double Fiw_bc = SimParam.k * (a.BodyRad - diw);
                            Vector2D Fiw_bcout = new Vector2D(Fiw_bc * niw.X, Fiw_bc * niw.Y);

                            double Fiw_sf = SimParam.kappa * (a.BodyRad - diw);

                            Vector2D tangVel = new Vector2D(a.Vo.X * tiw.X, a.Vo.Y * tiw.Y);

                            Vector2D Fiw_sfout = new Vector2D(Fiw_sf * tangVel.X * tiw.X, Fiw_sf * tangVel.Y * tiw.Y);


                            //% summation of Fiw %
                            tmpFiw = new Vector2D(Fiw_out.X - Fiw_sfout.X + Fiw_bcout.X, Fiw_out.Y - Fiw_sfout.Y + Fiw_bcout.Y);
                        }


                    }


                    Fiw.X += tmpFiw.X;
                    Fiw.Y += tmpFiw.Y;
                }

                foreach (ObstacleStruct obstacle in ObstaclesVertical)
                {

                    //Vector2D tmpFiw = SimHelper.Fiwobs(SimParam.A, SimParam.B, SimParam.k, SimParam.kappa, a.Po, obs, a.BodyRad, a.MinRange, a.Vo);
                    Vector2D tmpFiw;
                    {

                        Vector2D dv;
                        double diw;
                        if (a.Po.Y <= obstacle.StartPos.Y)
                        {
                            dv = new Vector2D(a.Po.X - obstacle.StartPos.X, a.Po.Y - obstacle.StartPos.Y);

                            diw = dv.GetMagnitude();
                        }
                        else if (a.Po.Y >= obstacle.EndPos.Y)
                        {
                            dv = new Vector2D(a.Po.X - obstacle.StartPos.X, a.Po.Y - obstacle.EndPos.Y);

                            diw = dv.GetMagnitude();
                        }
                        else
                        {
                            dv = new Vector2D(a.Po.X - obstacle.StartPos.X, 0);


                            diw = Math.Abs(dv.X);

                        }


                        Vector2D niw = dv.Normalize();
                        double C = (a.BodyRad - diw) / SimParam.B;
                        double tFiw = SimParam.A * Math.Exp(C);

                        Vector2D Fiw_out = new Vector2D(niw.X * tFiw, niw.Y * tFiw);

                        if (diw > a.BodyRad)
                        {

                            tmpFiw = Fiw_out;
                        }
                        else
                        {

                            Vector2D tiw = new Vector2D(-niw.Y, niw.X);

                            //% ----calculate repulsion force ----%

                            double Fiw_bc = SimParam.k * (a.BodyRad - diw);
                            Vector2D Fiw_bcout = new Vector2D(Fiw_bc * niw.X, Fiw_bc * niw.Y);

                            double Fiw_sf = SimParam.kappa * (a.BodyRad - diw);
                            Vector2D tangVel = new Vector2D(a.Vo.X * tiw.X, a.Vo.Y * tiw.Y);
                            Vector2D Fiw_sfout = new Vector2D(Fiw_sf * tangVel.X * tiw.X, Fiw_sf * tangVel.Y * tiw.Y);

                            //% summation of Fiw %
                            tmpFiw = new Vector2D(Fiw_out.X - Fiw_sfout.X + Fiw_bcout.X, Fiw_out.Y - Fiw_sfout.Y + Fiw_bcout.Y);
                        }


                    }


                    Fiw.X += tmpFiw.X;
                    Fiw.Y += tmpFiw.Y;
                }


                Vector2D Ftot = new Vector2D(Fo.X / a.Mass + Fij.X / a.Mass + Fiw.X / a.Mass, Fo.Y / a.Mass + Fij.Y / a.Mass + Fiw.Y / a.Mass);

 
                Vector2D Acc = new Vector2D(Ftot.X, Ftot.Y);


                a.Next_Vo.X = a.Vo.X + Acc.X * SimParam.dt;
                a.Next_Vo.Y = a.Vo.Y + Acc.Y * SimParam.dt;



                double nextVelMag = a.Next_Vo.GetMagnitude();
                if (nextVelMag > a.Vi)
                {

                    double nextValMul = a.Vi / nextVelMag;
                    a.Next_Vo.X *= nextValMul;
                    a.Next_Vo.Y *= nextValMul;


                }



                a.Next_Po.X = a.Po.X + a.Next_Vo.X * SimParam.dt;
                a.Next_Po.Y = a.Po.Y + a.Next_Vo.Y * SimParam.dt;

                /*
                if (a == testAgent)
                {


                    if (!Validator.SetValueA(a.NextPos.X, TickCount))
                    {
                        stopFlag = true;
                        Debug.Print("PoX:" + a.Po.X.ToString());
                        Debug.Print("PoY:" + a.Po.Y.ToString());
                    }

                }
                */
                a.Next_Vo.Rounding();
                a.Next_Po.Rounding();

                //average agent speed

                if (a.startMeasureSpeed)
                {
                    a.velocityTraveledInCorridor.Add(a.Next_Vo.GetMagnitude());
                    a.distanceTraveledInCorridor += a.Po.GetDistance(a.Next_Po);
                    a.totalTimeTraveledInCorridor += SimParam.dt;
                }

            };
        }

    }
    public struct AgentStruct
    {
        public Vector2D Po;
        public Vector2D Vo;

        public double BodyRad;
        public double Vi;
        public double Mass;
        public double MinRange;
        public double Kad;
        public double Epsilon;

        public byte ReachDestination;

        public Vector2D TargetPos;

        public Vector2D Next_Vo;
        public Vector2D Next_Po;
        public byte Next_ReachDestination;

        public int CurrentZoneIndex;
        public int IndexTag;

        public double distanceTraveledInCorridor;
        public double totalTimeTraveledInCorridor;
        public bool startMeasureSpeed;
    }
    public struct ObstacleStruct
    {
        public Vector2D StartPos;
        public Vector2D EndPos;

        public sbyte obType;
    }
    public struct ZoneStruct
    {
        public double DoorOffset;
        public byte DoorLocation; //0: WEST, 1:NORTH, 2: EAST, 3: SOUTH

        public Vector2D TopLeft;

        public Vector2D BottomRight;

        public int ExitZoneIndex;
        public int WallGeneration;

        public double DoorWidth;

        //TEMP VALUE USE
        public Vector2D FarExitTargetPosition; //Target position when agent is not at the door
        public Vector2D NearExitTargetPosition; //target position when agent is in door region
        public Vector2D DoorCenterPosition;

        public bool IsCorridor;
    }

}
