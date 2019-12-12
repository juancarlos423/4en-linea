using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Grid2D : MonoBehaviour
{

   
    public Color color1;                                                                     //declaracion de variable publica color 1
    public Color color2;                                                                     //declaracion de variable publica color 2 
    public Color color3;                                                                     //declaracion de variable publica color 3
    private int Turno =2;                                                                    //declaracion de variable privada de turno
    public Color colorfondo;                                                                 //declaracion de variable publica color fondo
    private GameObject[,] grid;                                                              //declaracion de variable privada de un objeto esfera
    public int height;                                                                       //declaracion de variable publica de altura
    public int width;                                                                        //declaracion de variable publica de ancho
    private int  Xaleatorio ;                                                                //declaracion de variable privada de aleatorio en y
    private int  Yaletaorio ;                                                                //declaracion de variable privada de aleatorio en x
    public float Tiempo = 3f;                                                                //declaracion de variable flotante de tiempo
    
    void Start()
    {
                    

        grid = new GameObject[width, height];                                               //el objeto se ubica en una posicion en altura y ancho
        for (int i = 0; i < width; i++)                                                     //se inicia el siclo de poner esferas a lo ancho
        {
            
            for (int j = 0; j < height; j++)                                                //se inicia el siclo de ponera esferas a lo alto
            {
               
                var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);                  //se almacena una un objeto tipo esfera
                go.transform.position= new Vector3(i,j,0);                                  //la esfera almacenada se ubica enuna pocicion en el vector 3
                grid[i,j]=go;                                                               //coordenadas del objeto en x ,y

                go.GetComponent<Renderer>().material.color = colorfondo;              //se crea un material de tipo color
             


                grid[i, j] = go;                                                            //el objeto grid es igual al a la variable go
            }

        }
    }

   



    void Update()

    {
        Tiempo -=Time.deltaTime;                                                             //timer
         Vector3 mPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);           //posicion del la camara en vector 
         UpdatePickedPiece(mPosition);                                                       //posicion de el mouse 
        
    }

    void UpdatePickedPiece(Vector3 position)
    {
        int i = (int)(position.x + 0.5f);                                                   //variable i se ubica en una pocicion x
        int j = (int)(position.y + 0.5f);                                                   //variable j se ubica en una pocicion y
        Xaleatorio = Random.Range (0,10);                                                   //variable genera posición aleatoria en eje x para la máquina
        Yaletaorio = Random.Range (0,10);                                                   //variable genera posición aleatoria en eje y para la máquina

        if (Input.GetButtonDown("Fire1"))
        {
            if (i >= 0 && j >= 0 && i < width && j< height)                                 //variable i y variable j se ubican alo ancho y a lo alto
            {
                GameObject go=grid[i,j];                                                    //el objeto se pone en el espacio i y en el j
               
                 
                if (go.GetComponent<Renderer>().material.color == colorfondo)               //se renderisa el color del fondo, en este caso gris.
                {
                    Color colorAUsar = Color.clear;                                         //define color que se usará según el usuario y reinicia la variable
                    if (Turno==1)
                    {
                        colorAUsar = color2;                                                //guarda el color a usar según el jugador en turno, en este caso el jugador 1.
                        Turno = 2;                                                          //modifica el valor de la variable Turno a 2, indicando que el siguiente turno será para el jugador 2
                    }                   
                    else
                    {
                            colorAUsar = color1;                                            //guarda el color a usar según el jugador en turno, en este caso el jugador 2.
                            Turno = 1;                                                      //modifica el valor de la variable Turno a 1, indicando que el siguiente turno será para el jugador 2
                        
                    }
                    
                  
                    go.GetComponent<Renderer>().material.color = colorAUsar;                //Renderiza el color del objeto.
                    go=grid[Xaleatorio,Yaletaorio];                                         //Ubicación de la esfera en el eje x y y para el jugador máquina
                    while( go.GetComponent<Renderer>().material.color == colorfondo )       //Ciclo se asegira que el color de la posición aleatoria, no sea, ni azul (jugador1), ni rojo (jugador2) 
                    {
                            go.GetComponent<Renderer>().material.color = color3;            //genera el color amarillo que es el de la máquina
                            Xaleatorio = Random.Range (0,10);                               //genera posición aleatoria en x, en caso de que el color haya sido juador 1 o juagador2
                            Yaletaorio = Random.Range (0,10);                               //genera posición aleatoria en y, en caso de que el color haya sido juador 1 o juagador2
                    }
                   
                    VerificadorX(i, j, colorAUsar);                                         //verificadores de eje x, eje y , diagonal positivo y diagonal negativo etc.
                    VerificadorX1(i, j, colorAUsar);
                    VerificadorX2(Xaleatorio, Yaletaorio, color3);
                    VerificadorY(i, j, colorAUsar);
                    VerificadorY1(i, j, colorAUsar);
                    VerificadorY2(Xaleatorio, Yaletaorio, color3);
                    DiagoPositiva(i, j, colorAUsar);
                    DiagoPositiva1(i, j, colorAUsar);
                    DiagoPositiva2(Xaleatorio, Yaletaorio, color3);
                    DiagoNegativa(i, j, colorAUsar);
                    DiagoNegativa1(i, j, colorAUsar);
                    DiagoNegativa2(Xaleatorio, Yaletaorio, color3);
                                     

                }
            }
        }
        
     
        
    }
    //procedimiento que permite ir contando las esferas consecutivas del juagor 1 en el eje x
    public void VerificadorX(int x, int y, Color colorVerificar)
    {
        int contador = 0;
        for (int i = x-3; i <= x+3; i++)
        {
            if (i < 0 || i >= width)
                continue;

            GameObject go = grid[i, y];

            if (go.GetComponent<Renderer>().material.color == colorVerificar)
            {
                if (go.GetComponent<Renderer>().material.color == color1)
                {
                contador++;
                    

                if (contador == 4)
                {
                    Debug.Log("ganaste jugador 1");
                    SceneManager.LoadScene(2);

                }
            }
            else
                contador = 0;
            }
        }
    }
         
         ////procedimiento que permite ir contando las esferas consecutivas del juagor 2 en el eje x
          public void VerificadorX1(int x, int y, Color colorVerificar)
    {
        int contador = 0;
        for (int i = x-3; i <= x+3; i++)
        {
            if (i < 0 || i >= width)
                continue;

            GameObject go = grid[i, y];

            if (go.GetComponent<Renderer>().material.color == colorVerificar)
            {
                if (go.GetComponent<Renderer>().material.color == color2)
                {
                    contador++;
                    if (contador == 4)
                    {
                        Debug.Log("ganaste jugador 2");
                        SceneManager.LoadScene(3);

                    }
                }
                else
                    contador = 0;
            }
        }
    }
           
           //procedimiento que permite ir contando las esferas consecutivas del juagor 3 en el eje x
           public void VerificadorX2(int x, int y, Color colorVerificar)
    {
        int contador = 0;
        for (int i = x-3; i <= x+3; i++)
        {
            if (i < 0 || i >= width)
                continue;

            GameObject go = grid[i, y];

            if (go.GetComponent<Renderer>().material.color == colorVerificar)
            {
                if (go.GetComponent<Renderer>().material.color == color3)
                {
                    contador++;
                    if (contador == 4)
                    {
                        Debug.Log("Gana maquina1");
                        SceneManager.LoadScene(4);

                    }
                }
                else
                    contador = 0;
            }
        }
    }

   //procedimiento que permite ir contando las esferas consecutivas del juagor 1 en el eje y
    public void VerificadorY(int x, int y, Color colorVerificar)
    {
        int contador = 0;
        for (int j = y - 3; j <= y + 3; j++)
        {
            if (j < 0 || j >= height)
                continue;

            GameObject go = grid[x, j];

            if (go.GetComponent<Renderer>().material.color == colorVerificar)
            {
                if (go.GetComponent<Renderer>().material.color == color1)
                {
                contador++;
                if (contador == 4)
                {
                    Debug.Log("ganaste juagador 1");
                    SceneManager.LoadScene(2);
                }
            }
            else
                contador = 0;
        }
        }
    }

    //procedimiento que permite ir contando las esferas consecutivas del juagor 2 en el eje y
     public void VerificadorY1(int x, int y, Color colorVerificar)
    {
        int contador = 0;
        for (int j = y - 3; j <= y + 3; j++)
        {
            if (j < 0 || j >= height)
                continue;

            GameObject go = grid[x, j];

            if (go.GetComponent<Renderer>().material.color == colorVerificar)
            {
                if (go.GetComponent<Renderer>().material.color == color2)
                {
                contador++;
                if (contador == 4)
                {
                    Debug.Log("ganaste juagador 2");
                    SceneManager.LoadScene(3);
                }
            }
            else
                contador = 0;
        }
        }
    }

    //procedimiento que permite ir contando las esferas consecutivas del juagor 3 en el eje y
    public void VerificadorY2(int x, int y, Color colorVerificar)
    {
        int contador = 0;
        for (int j = y - 3; j <= y + 3; j++)
        {
            if (j < 0 || j >= height)
                continue;

            GameObject go = grid[x, j];

            if (go.GetComponent<Renderer>().material.color == colorVerificar)
            {
                if (go.GetComponent<Renderer>().material.color == color3)
                {
                contador++;
                if (contador == 4)
                {
                    Debug.Log("Gana maquina2");
                    SceneManager.LoadScene(4);
                }
            }
            else
                contador = 0;
             }
        }
    }

    //procedimiento que permite ir contando las esferas consecutivas del juagor 1 en la diagonal positiva
    public void DiagoPositiva(int x, int y, Color colorAVerificando)
    {
        int contador = 0;
        int j = y - 4;


        for (int i = x - 3; i <= x + 3; i++ )
        {
            j++;
            if (j < 0 || j >= height || i < 0 || i >= width)
                continue;

                GameObject go =grid[i, j];
               
                //Debug.Log(go.GetComponent<Renderer>().material.color=color.yellow);
                //Debug.Log(colorAVerificando);
                if (go.GetComponent<Renderer>().material.color == colorAVerificando)
                {
                    if (go.GetComponent<Renderer>().material.color == color1)
                    {
                    contador++;
                    

                    if (contador == 4)
                    {
                        Debug.Log("ganaste jugador 1");
                        SceneManager.LoadScene(2); 
                }
                }
                else
                    contador = 0;
            //Debug.Log(contador);
                }
        }
    }

      //procedimiento que permite ir contando las esferas consecutivas del juagor 2 en la diagonal positiva
     public void DiagoPositiva1(int x, int y, Color colorAVerificando)
    {
        int contador = 0;
        int j = y - 4;


        for (int i = x - 3; i <= x + 3; i++ )
        {
            j++;
            if (j < 0 || j >= height || i < 0 || i >= width)
                continue;

                GameObject go =grid[i, j];
               
                //Debug.Log(go.GetComponent<Renderer>().material.color);
                //Debug.Log(colorAVerificando);
                if (go.GetComponent<Renderer>().material.color == colorAVerificando)
                {
                    if (go.GetComponent<Renderer>().material.color == color2)
                    {
                    contador++;
                    

                    if (contador == 4)
                    {
                        Debug.Log("ganaste jugador 2");
                        SceneManager.LoadScene(3); 
                }
                }
                else
                    contador = 0;
            //Debug.Log(contador);
                }
        }
    }

      //procedimiento que permite ir contando las esferas consecutivas del juagor 3 en la diagonal positiva
    public void DiagoPositiva2(int x, int y, Color colorAVerificando)
    {
        int contador = 0;
        int j = y - 4;


        for (int i = x - 3; i <= x + 3; i++ )
        {
            j++;
            if (j < 0 || j >= height || i < 0 || i >= width)
                continue;

                GameObject go =grid[i, j];
               
                //Debug.Log(go.GetComponent<Renderer>().material.color);
                //Debug.Log(colorAVerificando);
                if (go.GetComponent<Renderer>().material.color == colorAVerificando)
                {
                    if (go.GetComponent<Renderer>().material.color == color3)
                    {
                    contador++;
                    

                    if (contador == 4)
                    {
                        Debug.Log("Gana maquina3");
                        SceneManager.LoadScene(4); 
                }
                }
                else
                    contador = 0;
            //Debug.Log(contador);
                }
        }
    }
    
      //procedimiento que permite ir contando las esferas consecutivas del juagor 1 en la diagonal negativa
    public void DiagoNegativa(int x, int y, Color colorAVerificando)
    {
        int contador = 0;
        int j = y + 4;


        for (int i = x - 3; i <= x + 3; i++)
        {
            j--;

            if (j < 0 || j >= height || i < 0 || i >= width)
                continue;

            GameObject go = grid[i, j];

            if (go.GetComponent<Renderer>().material.color == colorAVerificando)
            {
                if (go.GetComponent<Renderer>().material.color == color1)
                {
                contador++;
               

                if (contador == 4)
                {
                    Debug.Log("ganaste juagador 1");
                    SceneManager.LoadScene(2);
                }
            }
            else
                contador = 0;
            }

        }
    }

    //procedimiento que permite ir contando las esferas consecutivas del juagor 2 en la diagonal negativa
    public void DiagoNegativa1(int x, int y, Color colorAVerificando)
    {
        int contador = 0;
        int j = y + 4;


        for (int i = x - 3; i <= x + 3; i++)
        {
            j--;

            if (j < 0 || j >= height || i < 0 || i >= width)
                continue;

            GameObject go = grid[i, j];

            if (go.GetComponent<Renderer>().material.color == colorAVerificando)
            {
                if (go.GetComponent<Renderer>().material.color == color2)
                {
                contador++;
               

                if (contador == 4)
                {
                    Debug.Log("ganaste jugador 2");
                    SceneManager.LoadScene(3);
                }
            }
            else
                contador = 0;
            }

        }
    }



     //procedimiento que permite ir contando las esferas consecutivas del juagor 3 en la diagonal negativa
    public void DiagoNegativa2(int x, int y, Color colorAVerificando)
    {
        int contador = 0;
        int j = y + 4;


        for (int i = x - 3; i <= x + 3; i++)
        {
            j--;

            if (j < 0 || j >= height || i < 0 || i >= width)
                continue;

            GameObject go = grid[i, j];

            if (go.GetComponent<Renderer>().material.color == colorAVerificando)
            {
                if (go.GetComponent<Renderer>().material.color == color3)
                {
                contador++;
               

                if (contador == 4)
                {
                    Debug.Log("Gana maquina4");
                    SceneManager.LoadScene(4);
                }
            }
            else
                contador = 0;
            }

        }
    }
}


