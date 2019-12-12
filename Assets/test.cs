using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private GameObject esfera;
    public bool checkbox;
    int num;
    


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EsferasColores());
    }

    public IEnumerator EsferasColores() { 
        Color[] color = new Color[6];
        num = Random.Range(3, 12);
        Color colorObjeto1 = Color.blue;
        Color colorObjeto2 = Color.red;

        color[0] = Color.yellow;
        color[1] = Color.blue;
        color[2] = Color.red;
        color[3] = Color.green;
        color[4] = Color.gray;
        color[5] = Color.cyan;
        
        if (checkbox == true)
        {

            for (int x = 0; x < num; x++)
            {
                GameObject objeto1 = null;
                for (int y = 0; y < num; y++)
                {
                    GameObject esfera = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    esfera.GetComponent<Renderer>().material.color = color[Random.Range(0,color.Length)];
                    esfera.transform.position =new Vector3(y, x, 0);
                    Comparadora comparar = new Comparadora();
                    if (objeto1 != null)
                    {
                        colorObjeto2 = esfera.GetComponent<Renderer>().material.color;
                        colorObjeto1 = objeto1.GetComponent<Renderer>().material.color;
                        esfera.GetComponent<Renderer>().material.color= comparar.colorActual(colorObjeto1,colorObjeto2);
                        objeto1.GetComponent<Renderer>().material.color = comparar.colorAnterior(colorObjeto1, colorObjeto2);
                    }

                    yield return new WaitForSeconds(0.5f);
                    objeto1 = esfera;
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
public class Comparadora
{

    public Color colorV = Color.black;
    // Start is called before the first frame update
    public Color colorAnterior(Color ant, Color prev)
    {
        if (ant == prev)
        {
            ant = colorV;
        }
        return ant;
  
    }
   public Color colorActual(Color ant, Color prev)
    {
        if (ant == prev)
        {
            prev= colorV;
        }
        return prev;
    }
        // Update is called once per frame
        void Update()
    {

    }
}




