using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesak.Simulation
{
    public class ZoneHelper
    {
        public static ZoneData[] LoadZones(string fileName)
        {
            List<ZoneData> zones = new List<ZoneData>();

            string[] s = File.ReadAllLines(fileName);


            for (int i = 0; i < s.Length; i++)
            {
                ZoneData zone = new ZoneData();

                string[] parameters = s[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                zone.ZoneLabel = parameters[0];
                zone.DoorLocation = int.Parse(parameters[1]);

                double tlx = double.Parse(parameters[2]);
                double tly = double.Parse(parameters[3]);
                zone.TopLeft = new Vector2D(tlx, tly);

                double brx = double.Parse(parameters[4]);
                double bry = double.Parse(parameters[5]);
                zone.BottomRight = new Vector2D(brx, bry);

                zone.ExitZoneIndex = int.Parse(parameters[6]);
                zone.WallGeneration = int.Parse(parameters[7]);
                zone.DoorOffset = double.Parse(parameters[8]);
                zones.Add(zone);
            }

            return zones.ToArray();
        }
        public static OldObstacle[] GenerateObstacles(ZoneData[] zones)
        {
            double[] y = new double[zones.Length];

            for (int i = 0; i < zones.Length; i++)
            {
                y[i] = zones[i].DoorOffset;
            }
            return GenerateObstacles(zones, y);
        }
        public static OldObstacle[] GenerateObstacles(ZoneData[] zones, double[] y)
        {
            List<OldObstacle> obstacles = new List<OldObstacle>();
            for (int i = 0; i < zones.Length; i++)
            {
                ZoneData zone = zones[i];

                if (zone.ExitZoneIndex == -1)
                    continue;

                //Create base wall
                bool[] wall = new bool[4];

                wall[0] = ((zone.WallGeneration & 0x08) != 0);
                wall[1] = ((zone.WallGeneration & 0x04) != 0);
                wall[2] = ((zone.WallGeneration & 0x02) != 0);
                wall[3] = ((zone.WallGeneration & 0x01) != 0);

                for (int j = 0; j < 4; j++)
                {
                    if (!wall[j])
                        continue;

                    OldObstacle obs = new OldObstacle();
                    switch (j)
                    {
                        case 0:
                            //vertical wall west
                            obs.StartPos = new Vector2D(zone.TopLeft.X, zone.TopLeft.Y);
                            obs.EndPos = new Vector2D(zone.TopLeft.X, zone.BottomRight.Y);
                            break;
                        case 1:
                            //horizontal wall top
                            obs.StartPos = new Vector2D(zone.TopLeft.X, zone.TopLeft.Y);
                            obs.EndPos = new Vector2D(zone.BottomRight.X, zone.TopLeft.Y);
                            break;
                        case 2:
                            //vertical wall east
                            obs.StartPos = new Vector2D(zone.BottomRight.X, zone.TopLeft.Y);
                            obs.EndPos = new Vector2D(zone.BottomRight.X, zone.BottomRight.Y);
                            break;
                        case 3:
                            //horizontal wall bottom
                            obs.StartPos = new Vector2D(zone.TopLeft.X, zone.BottomRight.Y);
                            obs.EndPos = new Vector2D(zone.BottomRight.X, zone.BottomRight.Y);
                            break;
                    }

                    obstacles.Add(obs);
                }


                OldObstacle a = new OldObstacle();
                OldObstacle b = new OldObstacle();
                //generate door
                double h = zone.TopLeft.Y - zone.BottomRight.Y;
                double doorh = h - zone.DoorWidth;

                double w = zone.BottomRight.X - zone.TopLeft.X;
                double doorw = w - zone.DoorWidth;
                double start;
                double end;
                double targetOffset = zone.DoorWidth / 2;
                switch (zone.DoorLocation)
                {
                    case 0: //West

                        start = (doorh * y[i]) + zone.BottomRight.Y;
                        end = (doorh * y[i]) + zone.BottomRight.Y + zone.DoorWidth;
                        zone.DoorCenterPosition = new Vector2D(zone.TopLeft.X, start + zone.DoorWidth / 2);

                        zone.FarExitTargetPosition = new Vector2D(zone.DoorCenterPosition.X + targetOffset, zone.DoorCenterPosition.Y);
                        zone.NearExitTargetPosition = new Vector2D(zone.DoorCenterPosition.X - targetOffset, zone.DoorCenterPosition.Y);

                        if (zone.BottomRight.Y != start)
                        {
                            a.StartPos = new Vector2D(zone.TopLeft.X, zone.BottomRight.Y);
                            a.EndPos = new Vector2D(zone.TopLeft.X, start);
                            obstacles.Add(a);
                        }

                        if (zone.TopLeft.Y != end)
                        {
                            b.StartPos = new Vector2D(zone.TopLeft.X, zone.TopLeft.Y);
                            b.EndPos = new Vector2D(zone.TopLeft.X, end);
                            obstacles.Add(b);
                        }

                        break;
                    case 1: //North
                        start = (doorw * y[i]) + zone.TopLeft.X;
                        end = (doorw * y[i]) + zone.TopLeft.X + zone.DoorWidth;

                        zone.DoorCenterPosition = new Vector2D(start + zone.DoorWidth / 2, zone.TopLeft.Y);

                        zone.FarExitTargetPosition = new Vector2D(zone.DoorCenterPosition.X, zone.DoorCenterPosition.Y - targetOffset);
                        zone.NearExitTargetPosition = new Vector2D(zone.DoorCenterPosition.X, zone.DoorCenterPosition.Y + targetOffset);

                        if (zone.TopLeft.X != start)
                        {
                            a.StartPos = new Vector2D(zone.TopLeft.X, zone.TopLeft.Y);
                            a.EndPos = new Vector2D(start, zone.TopLeft.Y);
                            obstacles.Add(a);
                        }

                        if (zone.BottomRight.X != end)
                        {
                            b.StartPos = new Vector2D(end, zone.TopLeft.Y);
                            b.EndPos = new Vector2D(zone.BottomRight.X, zone.TopLeft.Y);
                            obstacles.Add(b);
                        }
                        break;
                    case 2: //East

                        start = (doorh * y[i]) + zone.BottomRight.Y;
                        end = (doorh * y[i]) + zone.BottomRight.Y + zone.DoorWidth;

                        zone.DoorCenterPosition = new Vector2D(zone.BottomRight.X, start + zone.DoorWidth / 2);
                        zone.FarExitTargetPosition = new Vector2D(zone.DoorCenterPosition.X - targetOffset, zone.DoorCenterPosition.Y);
                        zone.NearExitTargetPosition = new Vector2D(zone.DoorCenterPosition.X + targetOffset, zone.DoorCenterPosition.Y);

                        if (zone.BottomRight.Y != start)
                        {
                            a.StartPos = new Vector2D(zone.BottomRight.X, zone.BottomRight.Y);
                            a.EndPos = new Vector2D(zone.BottomRight.X, start);
                            obstacles.Add(a);
                        }

                        if (zone.TopLeft.Y != end)
                        {
                            b.StartPos = new Vector2D(zone.BottomRight.X, zone.TopLeft.Y);
                            b.EndPos = new Vector2D(zone.BottomRight.X, end);
                            obstacles.Add(b);
                        }



                        break;
                    case 3: //South
                        start = (doorw * y[i]) + zone.TopLeft.X;
                        end = (doorw * y[i]) + zone.TopLeft.X + zone.DoorWidth;

                        zone.DoorCenterPosition = new Vector2D(start + zone.DoorWidth / 2, zone.BottomRight.Y);
                        zone.FarExitTargetPosition = new Vector2D(zone.DoorCenterPosition.X, zone.DoorCenterPosition.Y + targetOffset);
                        zone.NearExitTargetPosition = new Vector2D(zone.DoorCenterPosition.X, zone.DoorCenterPosition.Y - targetOffset);
                        if (zone.TopLeft.X != start)
                        {
                            a.StartPos = new Vector2D(zone.TopLeft.X, zone.BottomRight.Y);
                            a.EndPos = new Vector2D(start, zone.BottomRight.Y);
                            obstacles.Add(a);
                        }

                        if (zone.BottomRight.X != end)
                        {
                            b.StartPos = new Vector2D(end, zone.BottomRight.Y);
                            b.EndPos = new Vector2D(zone.BottomRight.X, zone.BottomRight.Y);
                            obstacles.Add(b);
                        }
                        break;
                }



            }

            return obstacles.ToArray();

        }
    }
}
