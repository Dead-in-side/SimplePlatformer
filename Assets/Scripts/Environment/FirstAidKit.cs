using UnityEngine;

public class FirstAidKit : MonoBehaviour
{
    public float HealPower { get; private set; } = 30;

   public void Desable()=>gameObject.SetActive(false);
}
