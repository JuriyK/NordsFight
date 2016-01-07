using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Controller : MonoBehaviour {

	bool N21 = false;
	bool N22 = false;
	bool runLoop = true;
	private Animation animN;
	private Animation animO;
	private GameObject alphaO;
	private GameObject alphaN;
    [Header("____Game Objects_____")]
	public GameObject Nord;
	public GameObject Orc;
	public GameObject AttackNord1;
	public GameObject AttackNord2;
	public GameObject SwordDust;
	public GameObject AttackOrc2;
	public GameObject AttackOrc1;
	public GameObject DeathFX;
	public GameObject UpLevelFx;
    public GameObject FlashOrc;
    public GameObject FlashNord;
	public GameObject Lt;
	public Light li;
   
    [Header("____Health and damage_____")]
	public float HealthNord;
	public float HealthOrc;
	public float damageNord;
	public float damage;
	public int skill;
	[Range(2f, 10.0f)]
	public float AttackWeitTime;
	[Header("____Fx SpawnPoints_____")]
	public Transform Attack1N1Fx;
	public Transform Attack1N2Fx;
	public Transform Attack2NordFx;
	public Transform SwordFx;
	public Transform DeathFXPointNord;
	[Space(10)]
	public Transform Attack1OLFx;
	public Transform Attack1ORFx;
	public Transform Attack2OrcFx;
	public Transform DeathFXPointOrc;
	public Transform UpLevelFxOrc;
	public Transform UpLevelFxNord;
	[Header("____UI_____")]
	public Slider healthSlider;
	public Slider healthSliderOrc;
	[Header("____Change color on hit_____")]
	public GameObject ColorHitOrc; 
	public GameObject ColorHitNord; 
	public Color colorHit = Color.red;
	public Color colorNormal;
	public Shader shader1;
	private float a;




	// Контроллер анимации и синхронизация с FX
    // Норд
	IEnumerator NordAnimLoop(float waitTime) {

		// Норд атака 2

		if (animN.IsPlaying("Attack2")) {

			N22 = true;
			N21 = true;

			while (N21) {
				if (animN ["Attack2"].time > 1.2) {	
					Instantiate (SwordDust, SwordFx.position, SwordFx.rotation);
					N21 = false;
				}
				yield return new WaitForSeconds (0F);
			}

			while (N22) {
				if (animN ["Attack2"].time > 2.0 ) {
					Instantiate (AttackNord2, Attack2NordFx.position, Attack2NordFx.rotation);
                    Instantiate(FlashNord, new Vector3(6.4F, -7, 2.2F), Quaternion.identity);
                    ApplyDamageNord(damageNord + skill);
					ColorHitOrc.GetComponent<Renderer>().material.color = colorHit;  // Меняем цвет на атаке

					li.color = Color.red;
					animO.CrossFade ("Hit");
					animO.CrossFadeQueued ("Idle");
					N22 = false;
				}
				yield return new WaitForSeconds (0F);
			}
		}

		// Норд атака 1

		if (animN.IsPlaying("Attack")) {
			N22 = true;
			N21 = true;
			
			while (N21) {
				if (animN ["Attack"].time > 0.5 && animN ["Attack"].time < 0.52) {					
					Instantiate (AttackNord1, Attack1N1Fx.position, Attack1N1Fx.rotation);
					ApplyDamageNord(damageNord);
					ColorHitOrc.GetComponent<Renderer>().material.color = colorHit;
					li.color = Color.red;
					animO.CrossFade ("Hit");
					animO.CrossFadeQueued ("Idle");
					N21 = false;
				}
				yield return new WaitForSeconds (0F);
			}
			
			while (N22) {
				if (animN ["Attack"].time > 1.1 && animN ["Attack"].time < 1.12) {					
					Instantiate (AttackNord1, Attack1N2Fx.position, Attack1N2Fx.rotation);
					N22 = false;
				}
				yield return new WaitForSeconds (0F);
			}
		}

		// Orc  атака 1

		if (animO.IsPlaying("Attack")) {
			N22 = true;
			N21 = true;

			while (N22) {
				if (animO ["Attack"].time > 0.3) {					
					Instantiate (AttackOrc1, Attack1ORFx.position, Attack1ORFx.rotation);
					N22 = false;
				}
				yield return new WaitForSeconds (0F);
			}
			while (N21) {
				if (animO ["Attack"].time > 0.7 ) {					
					Instantiate (AttackOrc1, Attack1OLFx.position, Attack1OLFx.rotation);
					ApplyDamage(damage);
					ColorHitNord.GetComponent<Renderer>().material.color = colorHit;
					li.color = Color.red;
					animN.CrossFade ("Hit");
					animN.CrossFadeQueued ("Idle");
					N21 = false;
				}
				yield return new WaitForSeconds (0F);
			}
		}

		// Orc атака 2

		if (animO.IsPlaying("Attack2")) {
			N22 = true;

			while (N22) {
				if (animO ["Attack2"].time > 0.8) {
					Instantiate (AttackOrc2, Attack2OrcFx.position, Attack2OrcFx.rotation);
                    Instantiate(FlashOrc, new Vector3( 6.4F, -7, 2.2F), Quaternion.identity);
                    ApplyDamage(damage + skill);
					ColorHitNord.GetComponent<Renderer>().material.color = colorHit;
					li.color = Color.red;
					animN.CrossFade ("Hit");
					animN.CrossFadeQueued ("Idle");
					N22 = false;
				}
				yield return new WaitForSeconds (0F);
			}
		}
		yield return new WaitForSeconds (0.2F);
		ColorHitOrc.GetComponent<Renderer>().material.color = colorNormal;
		ColorHitNord.GetComponent<Renderer>().material.color = colorNormal;
		li.color = Color.gray;
	}

	// Орк


	//Альфа и FX смерти
	IEnumerator DeathAlpha(float waitTime){

		if (HealthNord <= 0) {
			 
			yield return new WaitForSeconds (1F);
			Instantiate (DeathFX, DeathFXPointNord.transform.position, DeathFXPointNord.transform.rotation);
			Instantiate (UpLevelFx, UpLevelFxNord.transform.position, UpLevelFxNord.transform.rotation);
			ColorHitNord.GetComponent<Renderer>().material.shader = shader1;
			for (float i = 10; i >=0; i--) {
				a = i/10;
				alphaN.GetComponent<Renderer>().material.color = new Color (1,1,1,a);
				if (a <=0){
					ColorHitNord.GetComponent<SkinnedMeshRenderer>().enabled = false;
				}
				yield return new WaitForSeconds (0.05F);	
			}
		}
		if (HealthOrc <= 0) {
			yield return new WaitForSeconds (1F);
			Instantiate (DeathFX, DeathFXPointOrc.transform.position, DeathFXPointOrc.transform.rotation);
			Instantiate (UpLevelFx, UpLevelFxOrc.transform.position, UpLevelFxOrc.transform.rotation);
			ColorHitOrc.GetComponent<Renderer>().material.shader = shader1;
			for (float i = 10; i >=0; i--) {
				a = i/10;
				alphaO.GetComponent<Renderer>().material.color = new Color (1,1,1,a);
				if (a <=0){
					ColorHitOrc.GetComponent<SkinnedMeshRenderer>().enabled = false;
				}
				yield return new WaitForSeconds (0.05F);	
			}
		}	
		}



	// Старт
	void Start (){
		Set();


	}
	// Выполняем на старте
	void Set (){
		StartCoroutine (Health (0F)); // Обновляем хелз бар
		Nord = GameObject.FindWithTag("Nord");// Объявляем персонажей и компоненты
		Orc = GameObject.FindWithTag("Orc");
		animN = Nord.GetComponent<Animation>();
		animO = Orc.GetComponent<Animation>();
		alphaO = GameObject.Find("Orc_Defence:body");
		alphaN = GameObject.Find("Nord_Berserk_Mesh");
		healthSlider = GameObject.FindWithTag("HelthN").GetComponent<Slider>();
		healthSliderOrc = GameObject.FindWithTag("HelthO").GetComponent<Slider>();
		li = Lt.GetComponent<Light>();

	}

		
	// Demage & healthbar
	IEnumerator Health(float waitTime)
	{
		yield return new WaitForSeconds (0F);
		healthSliderOrc.value = HealthOrc;
		healthSlider.value = HealthNord;
	}

	void ApplyDamageNord(float damageNord){
		if (HealthOrc <= 0.0)
		return;
		StartCoroutine (Health (0F));
		
		HealthOrc -= damageNord;
		if (HealthOrc <= 0.0)
		{
			{
				Invoke("DeathO",0F);
			}
		}
	}
	void ApplyDamage(float damage)
	{
		
		if (HealthNord <= 0.0)
			return;
		StartCoroutine (Health (0F));
		
		HealthNord -= damage;
		if (HealthNord <= 0.0)
		{
			{
				Invoke("DeathN",0F);
			}
		}

	}
	void  DeathN(){	
		StartCoroutine (DeathAlpha (2F));
		animN.CrossFade ("Death");
		runLoop = false;
	}

	void  DeathO(){
		StartCoroutine (DeathAlpha (2.5F));
		animO.CrossFade ("Death");
		runLoop = false;
	}


	// Функции атаки
	// Норд


	void AttackN1(){
		if (runLoop == true) {
		animN.CrossFade ("Attack");
		animN.CrossFadeQueued ("Idle");
		StartCoroutine(NordAnimLoop(0F));
		Invoke("AttackO1", AttackWeitTime);
		}
	}

	void AttackN2(){
		if (runLoop == true) {
		animN.CrossFade ("Attack2");
		animN.CrossFadeQueued ("Idle");
		StartCoroutine(NordAnimLoop(0F));
		Invoke ("AttackO2", 4);
		}
	}

	// Орк

	void AttackO2(){
		if (runLoop == true) {
		animO.CrossFade ("Attack2");
		animO.CrossFadeQueued ("Idle");
		StartCoroutine(NordAnimLoop(0F));
		Invoke ("AttackN1", AttackWeitTime);
		}
	}
	void AttackO1(){
		if(runLoop == true) {
		animO.CrossFade ("Attack");
		animO.CrossFadeQueued ("Idle");
		StartCoroutine (NordAnimLoop (0F));
		Invoke ("AttackN2", AttackWeitTime);
		}
	}
	
}
	
	

			
			
		


		

	