using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticCustomMethodVector
{
    //Вычесляет точку пересечения двух прямых  
    //Используя для этого точку пересечения прямой и вектор напровления этой прямой
    // point1 - точка пересечения прямой, vector1 - вектор этой прямой
    // point2 - точка пересечения второй прямой, vector2 - вектор второй прямой
    public static bool FindPointIntersection(Vector2 point1, Vector2 vector1, Vector2 point2, Vector2 vector2, out Vector2 pointIntersection)
    {
        // Решаем систему уравнений для нахождения t и s:
        // P1 + t * A = P2 + s * B

        // Перепишем это в виде двух уравнений:
        // 1. P1.X + t * A.X = P2.X + s * B.X
        // 2. P1.Y + t * A.Y = P2.Y + s * B.Y

        // Перепишем систему:
        // t * A.X - s * B.X = P2.X - P1.X
        // t * A.Y - s * B.Y = P2.Y - P1.Y

        //Детерминант 
        double denominator = vector1.x * vector2.y - vector1.y * vector2.x; 

        if (denominator == 0)
        {
            // Если детерминант равен 0, прямые параллельны или совпадают.
            pointIntersection = Vector2.zero;
            return false;
        }

        double dx = point2.x - point1.x;
        double dy = point2.y - point1.y;

        // Вычисляем параметры t и s с помощью обратной матрицы
        double t = (dx * vector2.y - dy * vector2.x) / denominator;
        //не нужен для дальнейших вычеслений, но пусть будет(как пример того, как вычеслить)
        //double s = (dx * vector1.y - dy * vector1.x) / det;

        // Вычисляем точку пересечения
        Vector2 intersection = new Vector2((float)(point1.x + t * vector1.x), (float)(point1.y + t * vector1.y));

        pointIntersection = intersection;
        return true;
    }


    /// <summary>
    /// Вернет две точки пересечения двух линий
    /// 1) это точка пересечения на 1 линии
    /// 2) это точка пересечения на 2 линии
    /// (учитывать, что в unity forward считаеться от оси Z)
    /// point1 - точка пересечения прямой, vector1 - вектор этой прямой
    /// point2 - точка пересечения второй прямой, vector2 - вектор второй прямой
    /// </summary>
    /// <returns></returns>
    public static void FindPointIntersectionV3(Vector3 point1, Vector3 vector1, Vector3 point2, Vector3 vector2, out Vector3 pointIntersectionLine_1, out Vector3 pointIntersectionLine_2)
    {
        //P - точка(x,y,z)
		//V - вектор напровление(x,y,z)
		//T - кафицент который нужно найти для первого вектора
		//S - кафицент который нужно найти для второго вектора 

		//Общий вид формулы
		//P + T * V

		//Система для нахождения точек пересечения
		//P1.x + T * V1.x = P2.x + V2.x * S
		//P1.y + T * V1.y = P2.y + V2.y * S
		//P1.z + T * V1.z = P2.z + V2.z * S
		
		//Беру систему по X и нахожу как найти T
		//P1.x + T * V1.x = P2.x + V2.x * S
		//---------------------------------
		//T * V1.x = P2.x - P1.x + V2.x * S
		//---------------------------------
		//T = (P2.x - P1.x + V2.x * S) / V1.x
		
		//Теперь подстовляем найденное T в другую формулу (я выбираю формулу по оси Y)
		//P1.y + T * V1.y = P2.y + V2.y * S
		//---------------------------------
		//P1.y + (P2.x - P1.x + V2.x * S) / V1.x * V1.y = P2.y + V2.y * S
		//---------------------------------
		//Теперь выводим значение S из этой формулы
		//S = ((P2.y - P1.y) * V1.x - (P2.x - P1.x) * V1.y) / (V2.x * V1.y - V2.y * V1.x)
	
		//И так, сначало находим S
		//Затем находим T используя ранее найденную S
		//И наконец, по ранее описанной системе находим координаты пересечения, подставив в формулу наденные значения S и T 
		
		//Система для нахождения точек пересечения(дублирование)
		//P1.x + T * V1.x = P2.x + V2.x * S
		//P1.y + T * V1.y = P2.y + V2.y * S
		//P1.z + T * V1.z = P2.z + V2.z * S

		
		// Теперь тоже самое, если бы искал сначало S, а только затем находил бы T через S 
		// P1.x + T * V1.x = P2.x + V2.x * S
		// S = (P1.x - P2.x + V1.x * T) / V2.x
		//
		// P1.y + T * V1.y = P2.y + V2.y * S
		// P1.y + T * V1.y = P2.y + V2.y * (P1.x - P2.x + V1.x * T) / V2.x
		// T = ((P2.y - P1.y) * V2.x + (P1.x - P2.x) * V2.y) / (V1.y * V2.x - V1.x * V2.y)

		float s = 0f;
        float t = 0f;

	    //Точка пересечения на линии 1
        Vector3 pointIntersectionLine1;
        //Точка пересечения на линии 2
        Vector3 pointIntersectionLine2;
	        
	    s = ((point2.y - point1.y) * vector1.x - (point2.x - point1.x) * vector1.y) / (vector2.x * vector1.y - vector2.y * vector1.x);
        //Debug.Log("TEST V2 S = " + s);
        //работает, но не всегда !!!!!!!!!!!!!!!!!!(по этому нахожу по другому)
	    //t = ((point2.x - point1.x) + vector2.x * s) / vector1.x;
        //Debug.Log("TEST V2 T = " + t);
        
	    //pointIntersectionLine1 = new Vector3((point1.x + vector1.x * t), (point1.y + vector1.y * t), (point1.z + vector1.z * t));
	    pointIntersectionLine2 = new Vector3((point2.x + vector2.x * s), (point2.y + vector2.y * s), (point2.z + vector2.z * s));
        
        //Debug.Log("TEST Point V2 X1 = " + pointIntersectionLine1.x + " = " + pointIntersectionLine2.x);
        //Debug.Log("TEST Point V2 Y1 = " + pointIntersectionLine1.y + " = " + pointIntersectionLine2.y);
        //Debug.Log("TEST Point V2 Z1 = " + pointIntersectionLine1.z + " = " + pointIntersectionLine2.z);
        
	    t =  ((point2.y - point1.y) * vector2.x + (point1.x - point2.x) * vector2.y) / (vector1.y * vector2.x - vector1.x * vector2.y);
	    //Debug.Log("TEST V2.2 T = " + t);
	    //работает, но не всегда !!!!!!!!!!!!!!!!!!(по этому нахожу по другому)
	    //s = ((point1.x - point2.x) + vector1.x * t) / vector2.x;
	    //Debug.Log("TEST V2.2 S = " + s);
		 
		 pointIntersectionLine1 = new Vector3((point1.x + vector1.x * t), (point1.y + vector1.y * t), (point1.z + vector1.z * t));
		 //pointIntersectionLine2 = new Vector3((point2.x + vector2.x * s), (point2.y + vector2.y * s), (point2.z + vector2.z * s));
		 
         // Debug.Log("TEST Point V2.2 X1 = " + pointIntersectionLine1.x + " = " + pointIntersectionLine2.x);
         // Debug.Log("TEST Point V2.2 Y1 = " + pointIntersectionLine1.y + " = " + pointIntersectionLine2.y);
         // Debug.Log("TEST Point V2.2 Z1 = " + pointIntersectionLine1.z + " = " + pointIntersectionLine2.z);
         
        pointIntersectionLine_2 = pointIntersectionLine2;
        pointIntersectionLine_1 = pointIntersectionLine1;
    }

    /// <summary>
    /// Выведет информация об точках пересечения по ЧЕЛОВЕЧЕСКИ
    /// С учетом указанной точности(кол-во знаков после запятой, которое будет учитываться)
    /// 
    /// precision - кол - во знаком после запятой которое будет учитываеться
    /// где значение 1 = 0.1; 2 = 0.01; (и да 0 = 1, т.к это степень)
    /// </summary>
    public static void LinePointIntersectionInfo(Vector3 pointIntersectionLine1 , Vector3 pointIntersectionLine2, int precision)
    {
	    if (ComparePrecisionFractionalValue(pointIntersectionLine1.y, pointIntersectionLine2.y, precision) == false) 
	    {
		    if (pointIntersectionLine1.y > pointIntersectionLine2.y) 
		    {
			    Debug.Log("линия 2 проходит под линией 1 и не пересекает её по оси y");
		    }
		    else
		    {
			    Debug.Log("линия 2 проходит над линией 1 и не пересекает её по оси y");    
		    }
	    }
	    else
	    {
		    Debug.Log("линии пересекаються в одной точке по оси y");
	    }
        
	    if (ComparePrecisionFractionalValue(pointIntersectionLine1.x, pointIntersectionLine2.x, precision) == false)
	    {
		    if (pointIntersectionLine1.x > pointIntersectionLine2.x) 
		    {
			    Debug.Log("линия 2 проходит левее линии 1 и не пересекает её по оси x");
		    }
		    else
		    {
			    Debug.Log("линия 2 проходит правее линии 1 и не пересекает её по оси x");    
		    }
	    }
	    else
	    {
		    Debug.Log("линии пересекаються в одной точке по оси x");
	    }
        
	    if (ComparePrecisionFractionalValue(pointIntersectionLine1.z, pointIntersectionLine2.z, precision) == false)
	    {
		    if (pointIntersectionLine1.z > pointIntersectionLine2.z) 
		    {
			    Debug.Log("линия 2 проходит сзади линии 1 и не пересекает её по оси z");
		    }
		    else
		    {
			    Debug.Log("линия 2 проходит спереди линии 1 и не пересекает её по оси z");    
		    }
	    }
	    else
	    {
		    Debug.Log("линии пересекаються в одной точке по оси z");
	    }
    }

    /// <summary>
    /// Вернет True, если разница между дробными(float) значениями меньше, чем указанная точность
    ///  
    /// precision - кол - во знаком после запятой которое будет учитываеться
    /// где значение 1 = 0.1; 2 = 0.01; (и да 0 = 1, т.к это степень)
    /// </summary>
    public static bool ComparePrecisionFractionalValue(float a,float b, int precision)
    {
	    float value = Mathf.Abs(a - b);
	    float DecimalPrecision = Mathf.Pow(10, -precision);

	    if (value < DecimalPrecision) 
	    {
		    return true;
	    }

	    return false;
    }
}
