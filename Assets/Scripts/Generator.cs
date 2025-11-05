using UnityEngine;

public class Generator : MonoBehaviour{

    [SerializeField] private GameObject Piece;
    [SerializeField] public int width, height, bombsNumber;
    [SerializeField] public GameObject [][] map;

    public static Generator gen;
    private void Awake(){

        gen = this;
    }
    public void setWidth(int width) {
        
        this.width = width;
    }
    public void setHeight(int height){

        this.height = height;
    }
    public void setBombs(int bombs){

        this.bombsNumber = bombs;
    }
    public int Validate() {

        int errorCode = 0;

        if(width <= 1)
            errorCode += 4;
        if (height <= 1)
            errorCode += 2;
        if (!(bombsNumber >=0 && bombsNumber < (width*height)))
            errorCode += 1;

        return errorCode;
    }
    public void Generate(){

        gen = this;

        map = new GameObject[width][];

        for (int i = 0; i < map.Length; i++){
        
            map[i] = new GameObject[height];
        }

        for (int j = 0; j < height; j++){
            for (int i = 0; i < width; i++){

                map[i][j] = Instantiate(Piece, new Vector3(i, j, 0), Quaternion.identity);
                map[i][j].GetComponent<Piece>().setX(i);
                map[i][j].GetComponent<Piece>().setY(j);
            }
        }
        // centra la camara
        Camera.main.transform.position = new Vector3((float)width / 2 -0.5f, (float)height / 2 -0.5f, -10);

        for (int i = 0; i < bombsNumber; i++){

            int x = Random.Range(0, width);
            int y = Random.Range(0, height);

            if (!map[x][y].GetComponent<Piece>().isBomb()){

                map[x][y].GetComponent<Piece>().setBomb(true);
            }
            else {
                i--;
            }
        }
    }

    public int GetBombsAround(int x, int y) {

        int cont = 0;

        if (x > 0 && y < height - 1 && map[x - 1][y + 1].GetComponent<Piece>().isBomb()) cont++;
        // Arriba
        if (y < height - 1 && map[x][y + 1].GetComponent<Piece>().isBomb()) cont++;
        // Arriba derecha
        if (x < width - 1 && y < height - 1 && map[x + 1][y + 1].GetComponent<Piece>().isBomb()) cont++;
        // Izquierda
        if (x > 0 && map[x - 1][y].GetComponent<Piece>().isBomb()) cont++;
        // Derecha
        if(x < width - 1 && map[x + 1][y].GetComponent<Piece>().isBomb()) cont++;
        // Abajo izquierda
        if (x > 0 && y > 0 && map[x - 1][y - 1].GetComponent<Piece>().isBomb()) cont++;
        // Abajo
        if (y > 0 && map[x][y - 1].GetComponent<Piece>().isBomb()) cont++;
        // Abajo derecha
        if (x < width - 1 && y > 0 && map[x + 1][y - 1].GetComponent<Piece>().isBomb()) cont++;

        return cont;
    }

    public void CheckPieceAround(int x, int y) {

        if (x > 0 && y < height - 1) 
            map[x - 1][y+1].GetComponent<Piece>().DrawBomb();
        // Arriba
        if (y < height - 1) 
            map[x][y+1].GetComponent<Piece>().DrawBomb();
        // Arriba derecha
        if (x < width - 1 && y < height - 1) 
            map[x + 1][y+1].GetComponent<Piece>().DrawBomb();
        // Izquierda
        if (x > 0) 
            map[x - 1][y].GetComponent<Piece>().DrawBomb();
        // Derecha
        if (x < width - 1) 
            map[x + 1][y].GetComponent<Piece>().DrawBomb();
        // Abajo izquierda
        if (x > 0 && y > 0) 
            map[x - 1][y-1].GetComponent<Piece>().DrawBomb();
        // Abajo
        if (y > 0)
            map[x][y - 1].GetComponent<Piece>().DrawBomb();
        // Abajo derecha
        if (x < width - 1 && y > 0) 
            map[x + 1][y - 1].GetComponent<Piece>().DrawBomb();
    }

    public void DestroyMap() {

        if (map != null) {
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) { 
                    Destroy(map[i][j]);
                }
            }
        }
    }
    public int getBombs() {

        return bombsNumber;
    }
}