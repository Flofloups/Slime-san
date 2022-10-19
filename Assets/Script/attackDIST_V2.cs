using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackDIST_V2 : MonoBehaviour {

    // SCRIPT A METTRE SUR LE JOUEUR, 
    // Il a besoin d'une arme (Exemple : Un pistolet) qui sera enfant de votre personnage et qui servira de point de départ du projectile
    // Il marche en combinaison avec un projectile (qui sera un object avec un visuel, un trigger et un rigidbody. Exemple : un boulet de canon)
    // Ce script permet de tirer en direction de la souris

    public int degats = 1;
    public Transform weapon;                    // L'object qui va tirer votre projectile, doit être en enfant de votre personnage (exemple : Un pistolet)
    private Vector3 positionWeapon;             // Les coordonnées de l'arme, nous servira a positionner correctement l'arme quand on regarde a gauche
    public GameObject projectil;                // Le prefab du projectile que l'on tir, on doit glisser dans cette case un prefab avec un trigger ET un rigidbody2D
    private GameObject projectilSave;           // Une sauvegarde temporaire du projectile tiré pour lui apporté quelques modification quand on l'invoque
    private int pronum = 2;
    public AudioSource tickSource;

    public float speedProjectil = 10f;           // La vitesse de déplacement de notre projectile (valeur de base = 1)

    public float reloadTime = 1f;             // Le temps de chargement entre 2 tirs (valeur de base = 0.5)
    private bool reloading;                     // Booléen qui devient vrai le temps qu'on recharge

    private Vector3 mousePos;                   // Vector3 pour stocker la position de la souris
    private Vector3 direction;                  // Vector3 pour calculer la direction du projectile
    private float angleProjectil;               // rotation que devra avoir projectile pour "regarder" dans la direction ou il va

    private SpriteRenderer skin;                // Le sprite du joueur, on va s'en servir pour savoir si il regarde à gauche ou a droite

    private float radius = 5f;

    private Animator anim;                      // L'animator du joueur, ça nous permettra de lancer l'animation d'attaque quand on va tirer
    private Quaternion step;

    void Start() {
        if (!PlayerPrefs.HasKey("degatDIST")) {
            PlayerPrefs.SetInt("degatDIST", degats);
        }
        degats = PlayerPrefs.GetInt("degatDIST");
        skin = GetComponent<SpriteRenderer>();  // On récupère le sprite renderer du personnage (pour savoir dans quelle direction il regarde)
        anim = GetComponent<Animator>();        // On récupère son animator pour lancer l'animation d'attaque
        positionWeapon = weapon.localPosition;  // On enregistre la position local de l'arme, on l'utilisera pour retourner l'arme quand le joueur regarde à gauche
        tickSource = GetComponent<AudioSource>();
    }

    void Update() {

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // On récupère la position de la souris et on transforme ça en coordonnée local dans le jeu
        mousePos.z = transform.position.z;                              // On dit que la position en Z de la souris est égale à celle du joueur (pour être sur que le projectile sera sur le même plan que le personnage)
        direction = mousePos - weapon.position;                         // Cacule basique d'une direction : (position de votre cible) - (votre position) =  direction entre vous et la cible
        direction.Normalize();                                          // On donne à la direction une longueur de 1 mètre, plus simple pour faire nos future calcules
        angleProjectil = Vector3.SignedAngle(transform.right, direction, Vector3.forward);  // Maintenant qu'on a la direction de la souris, on calcul l'angle entre cette direction et la droite du joueur
        weapon.rotation = Quaternion.Euler(0, 0, angleProjectil);       // On fait pivoter l'arme dans la direction de la souris grace à l'angle qu'on vient de calculer.

        if (!skin.flipX) {
            weapon.localPosition = positionWeapon;      // Si on regarde a droite, l'arme prend sa position de base
        }

        if (skin.flipX) {
            weapon.localPosition = new Vector3 (-positionWeapon.x, positionWeapon.y, 0);    // Si on regarde a gauche, on inverse la position X (pas Y) de l'arme
        }

        // Bien maintenant qu'on connait dans quelle direction le joueur vise, on check si le joueur appuis sur son bouton de tir (ici clic-droit) ET qu'il n'est pas entrain de recharger (reloading = false)
        if (Input.GetMouseButton(0) && !reloading) {
            tickSource.Play();
            //degats = degats;
            //degats = PlayerPrefs.GetInt("degatDIST");
            //anim.SetTrigger("attackDIST"); // On lance l'animation d'attaque, si vous n'avez pas d'animation d'attaque sur votre personnage vous pouvez supprimer cette ligne (il doit exister un trigger "attackDIST" dans les parameter de votre animator)

        float angleStep = 360f;
		float angle = 0f;

            reloading = true;              // On passe directement reloading en vrai histoire de ne pas pouvoir tirer 2 fois de suite
            for (int i = 1; i < pronum; i++){

                float projectileDirXposition = positionWeapon.x + Mathf.Sin ((angle * Mathf.PI) / 180) * radius;
			    float projectileDirYposition = positionWeapon.y + Mathf.Cos ((angle * Mathf.PI) / 180) * radius;
                step = Quaternion.Euler(0, 0, 9);

                if(i==1){
                    projectilSave = Instantiate(projectil, weapon.position, Quaternion.Euler(0, 0, angleProjectil));    // on fait apparaitre le projectile, sur la position de votre arme (weapon) et pivoter avec l'angle qu'on a calculé plus haut
                    projectilSave.GetComponent<Rigidbody2D>().velocity = direction * speedProjectil;                    // Et on fait avancer le projectile dans la direction qu'on a calculé plutôt
                    projectilSave.GetComponent<projectile>().degats = degats;
                    StartCoroutine(waitShoot());}
                if(i > 1){
                    if(i % 2 == 0){
                        float temporaire = angleProjectil+((i/2)*9);
                        Quaternion temporaire1 = Quaternion.Euler(0, 0, temporaire);
                        float temporaire2 = temporaire1.eulerAngles.z * Mathf.Deg2Rad;
                        direction = new Vector3(Mathf.Cos(temporaire2), Mathf.Sin(temporaire2), 0f);
                        projectilSave = Instantiate(projectil, weapon.position, Quaternion.Euler(0, 0, temporaire));
                    }
                    else{
                        float temporaire = angleProjectil-(((i/2)-0.5f)*9);
                        Quaternion temporaire1 = Quaternion.Euler(0, 0, temporaire);
                        float temporaire2 = temporaire1.eulerAngles.z * Mathf.Deg2Rad;
                        direction = new Vector3(Mathf.Cos(temporaire2), Mathf.Sin(temporaire2), 0f);
                        projectilSave = Instantiate(projectil, weapon.position, Quaternion.Euler(0, 0, temporaire));
                    }
                    projectilSave.GetComponent<Rigidbody2D>().velocity = direction * speedProjectil;
                    projectilSave.GetComponent<projectile>().degats = degats;
                    StartCoroutine(waitShoot());}
                }
        }
        if(Input.GetKeyDown(KeyCode.V)){
            reloadTime = reloadTime/ 2;}
        if(Input.GetKeyDown(KeyCode.B)){
            degats = degats+ 2;}
        if(Input.GetKeyDown(KeyCode.N)){
            speedProjectil = speedProjectil* 1.4f;}
        if(Input.GetKeyDown(KeyCode.C)){
            pronum = pronum + 2;}
    }

    // Voici la coroutine waitShoot
    IEnumerator waitShoot() {
        yield return new WaitForSeconds(reloadTime); // La on dit au script de patienter pendant un certain temps (reloadTime)
        reloading = false;                           // On a fini d'attendre donc on repasse reloading en vrai, donc on va pouvoir tirer à nouveau
    }    
}                         //direction = Vector3.forward * Quaternion.Euler(0, 0, temporaire);
