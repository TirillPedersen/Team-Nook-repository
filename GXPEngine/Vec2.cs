using System;

namespace GXPEngine
{
    public struct Vec2
    {
        public float x;
        public float y;

        public Vec2(float pX = 0, float pY = 0)
        {
            x = pX;
            y = pY;
        }

        public static Vec2 operator +(Vec2 left, Vec2 right)
        {
            return new Vec2(left.x + right.x, left.y + right.y);
        }

        public static Vec2 operator -(Vec2 left, Vec2 right)
        {
            return new Vec2(left.x - right.x, left.y - right.y);
        }

        public static Vec2 operator *(float scalar, Vec2 vector)
        {
            return new Vec2(scalar * vector.x, scalar * vector.y);
        }

        public static Vec2 operator *(Vec2 vector, float scalar)
        {
            return new Vec2(scalar * vector.x, scalar * vector.y);
        }

        public float Length()
        {
            return Mathf.Sqrt(x * x + y * y);
        }

        public void Normalize()
        {
            float tempLength = this.Length();
            try
            {
                this.x *= (1 / tempLength);
                this.y *= (1 / tempLength);
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine("A division by zero is not valid!" + e);
            }
        }

        public Vec2 Normalized()
        {
            float tempLength = this.Length();
            try
            {
                return new Vec2(this.x *= (1 / tempLength), this.y *= (1 / tempLength));
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine("New reference has intitialized values, because a division by zero is not valid" + e);
                return new Vec2(0, 0);
            }
        }

        public void SetXY(float givenX, float givenY)
        {
            this.x = givenX;
            this.y = givenY;
        }

        public override string ToString()
        {
            return String.Format("({0},{1})", x, y);
        }

        public static float Deg2Rad(float givenDeg)
        {
            return givenDeg *= (float)(Math.PI / 180);
        }

        public static float Rad2Deg(float givenRad)
        {
            return givenRad *= (float)(180 / Math.PI);
        }

        public static Vec2 GetUnitVectorDeg(float givenDeg)
        {
            float tempDegInRad = Deg2Rad(givenDeg);
            return new Vec2(Mathf.Cos(tempDegInRad), Mathf.Sin(tempDegInRad));
        }

        public static Vec2 GetUnitVectorRad(float givenRad)
        {
            return new Vec2(Mathf.Cos(givenRad), Mathf.Sin(givenRad));
        }

        public static Vec2 RandomUnitVector()
        {
            float randomAngle = Deg2Rad(Utils.Random(0, 361));
            return new Vec2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
        }

        public void SetAngleDegrees(float givenDeg)
        {
            float tempVecLength = this.Length();
            x = Mathf.Cos(Deg2Rad(givenDeg)) * tempVecLength;
            y = Mathf.Sin(Deg2Rad(givenDeg)) * tempVecLength;
        }

        public void SetAngleRadians(float givenRad)
        {
            float tempVecLength = this.Length();
            x = Mathf.Cos(givenRad) * tempVecLength;
            y = Mathf.Sin(givenRad) * tempVecLength;
        }

        public float GetAngleRadians()
        {
            float tempAngle = Mathf.Atan2(y, x);
            if (tempAngle < 0) return (2 * (float)Math.PI) + tempAngle;
            else return tempAngle;
        }

        public float GetAngleDegrees()
        {
            float tempAngle = Rad2Deg(Mathf.Atan2(y, x));
            if (tempAngle < 0) return 360 + tempAngle;
            else return tempAngle;
        }

        public void RotateDegrees(float givenDeg)
        {
            float tempDegInRad = Deg2Rad(givenDeg);
            float prevX = x, prevY = y;

            x = (prevX * Mathf.Cos(tempDegInRad)) - (prevY * Mathf.Sin(tempDegInRad));
            y = (prevX * Mathf.Sin(tempDegInRad)) + (prevY * Mathf.Cos(tempDegInRad));
        }

        public void RotateRadians(float givenRad)
        {
            float prevX = x, prevY = y;

            x = (prevX * Mathf.Cos(givenRad)) - (prevY * Mathf.Sin(givenRad));
            y = (prevX * Mathf.Sin(givenRad)) + (prevY * Mathf.Cos(givenRad));
        }

        public void RotateAroundDegrees(Vec2 givenPoint, float givenDeg)
        {
            float tempDegInRad = Deg2Rad(givenDeg);

            x -= givenPoint.x;
            y -= givenPoint.y;

            RotateRadians(tempDegInRad);

            x += givenPoint.x;
            y += givenPoint.y;
        }

        public void RotateAroundRadians(Vec2 givenPoint, float givenRad)
        {
            x -= givenPoint.x;
            y -= givenPoint.y;

            RotateRadians(givenRad);

            x += givenPoint.x;
            y += givenPoint.y;
        }

        public float Dot(Vec2 secondVec)
        {
            return x * secondVec.x + y * secondVec.y;
        }

        public Vec2 Normal()
        {
            Vec2 unitNormal = new Vec2(-y, x);
            unitNormal.Normalize();
            return unitNormal;
        }

        public void Reflect(float COR, Vec2 other)
        {
            this = this - ((1 + COR) * (Dot(other.Normal()) * other.Normal()));
        }

        public static void UnitTest()
        {
            Vec2 testVec = new Vec2(2, 4);
            Vec2 testVec2 = new Vec2(3, 4);
            float tempFloat = 0;

            Vec2 temp = testVec + testVec2;
            Console.WriteLine("Addition correct?: " + (Math.Abs(temp.x - 5) < 0.01f && Math.Abs(temp.y - 8) < 0.01f));

            temp = testVec - testVec2;
            Console.WriteLine("Substraction correct?: " + (Math.Abs(temp.x + 1) < 0.01f && Math.Abs(temp.y) < 0.01f));

            temp = 2 * testVec;
            Console.WriteLine("Multiplication on left correct?: " + (Math.Abs(temp.x - 4) < 0.01f && Math.Abs(temp.y - 8) < 0.01f));

            temp = testVec * 2;
            Console.WriteLine("Multiplication on right correct?: " + (Math.Abs(temp.x - 4) < 0.01f && Math.Abs(temp.y - 8) < 0.01f));

            tempFloat = testVec2.Length();
            Console.WriteLine("Length correct?: " + (Math.Abs(tempFloat - 5) < 0.01f));

            testVec2.Normalize();
            Console.WriteLine("Normalize correct?: " + (Math.Abs(testVec2.Length() - 1) < 0.01f && Math.Abs(testVec2.x - 0.6f) < 0.01f && Math.Abs(testVec2.y - 0.8f) < 0.01f));

            testVec2 = new Vec2(3, 4);

            temp = testVec2.Normalized();
            Console.WriteLine("Normalized correct?: " + (Math.Abs(temp.Length() - 1) < 0.01f && Math.Abs(temp.x - 0.6f) < 0.01f && Math.Abs(temp.y - 0.8f) < 0.01f));

            testVec.SetXY(15, 2);
            Console.WriteLine("SetXY correct?: " + (Math.Abs(testVec.x - 15) < 0.01f && Math.Abs(testVec.y - 2) < 0.01f));

            Console.WriteLine("Deg2Rad correct?: " + (Math.Abs(Deg2Rad(180) - Math.PI) < 0.01f));

            Console.WriteLine("Rad2Deg correct?: " + (Math.Abs(Rad2Deg((float)Math.PI) - 180) < 0.01f));

            testVec = GetUnitVectorDeg(180);
            Console.WriteLine("GetUnitVectorDeg correct?: " + (Math.Abs(testVec.x + 1) < 0.01f && Math.Abs(testVec.y) < 0.01f));

            testVec = GetUnitVectorRad((float)Math.PI);
            Console.WriteLine("GetUnitVectorRad correct?: " + (Math.Abs(testVec.x + 1) < 0.01f && Math.Abs(testVec.y) < 0.01f));

            Console.WriteLine("RandomUnitVector correct?: " + (Math.Abs(RandomUnitVector().Length() - 1) < 0.01f));

            testVec = RandomUnitVector();
            testVec.SetAngleDegrees(270);
            Console.WriteLine("SetAngleDegrees correct?: " + (Math.Abs(testVec.y + 1) < 0.01f && Math.Abs(testVec.x) < 0.01f));

            Console.WriteLine("GetAngleDegrees correct?: " + (Math.Abs(testVec.GetAngleDegrees() - 270) < 0.01f));

            testVec.SetAngleRadians((float)Math.PI);
            Console.WriteLine("SetAngleRadians correct?: " + (Math.Abs(testVec.x + 1) < 0.01f && Math.Abs(testVec.y) < 0.01f));

            Console.WriteLine("GetAngleRadians correct?: " + (Math.Abs(testVec.GetAngleRadians() - Math.PI) < 0.01f));

            testVec.RotateDegrees(90);
            Console.WriteLine("RotateDegrees correct?: " + (Math.Abs(testVec.GetAngleDegrees() - 270) < 0.01f));

            testVec = RandomUnitVector();
            testVec.SetAngleDegrees(180);

            testVec.RotateRadians((float)Math.PI / 2);
            Console.WriteLine("RotateRadians correct?: " + (Math.Abs(testVec.GetAngleRadians() - (3 * Math.PI) / 2) < 0.01f));

            testVec = new Vec2(2, 4);
            testVec2 = new Vec2(3, 4);

            Console.WriteLine("Dot correct?: " + ((testVec.Dot(testVec2) - 22) < 0.01f));

            Vec2 testVecNormal = testVec2.Normal();
            Console.WriteLine("Normal correct?: " + (Math.Abs(testVecNormal.x + 0.8f) < 0.01f && Math.Abs(testVecNormal.y - 0.6f) < 0.01f && Math.Abs(testVecNormal.Length() - 1) < 0.01f));

            testVec.Reflect(1, testVec2);
            Console.WriteLine("Reflect correct?: " + (Math.Abs(testVec.x - 3.28f) < 0.01f && Math.Abs(testVec.y - 3.04f) < 0.01f));
        }
    }
}