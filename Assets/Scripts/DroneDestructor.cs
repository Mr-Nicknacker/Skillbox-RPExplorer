using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneDestructor : MonoBehaviour
{
    [SerializeField] private GameObject _droneMesh;
    private Transform[] _meshPartsArr;
    private List<Transform> _meshPartsList=new();

    [Header("Explosion parameters")]
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explodedPartsDestroyTime;
    [SerializeField] private float _explosionUpwardsModifier = 0f;

    public void Detonate()
    {
        Explode();

    }
    private void Explode()
    {
        _meshPartsArr = _droneMesh.GetComponentsInChildren<Transform>(true);
        foreach (Transform part in _meshPartsArr)
        {

            if (part == _droneMesh.transform) { continue; }
            _meshPartsList.Add(part);
            part.parent = null;

            Rigidbody meshPartRigidBody = InitializePartsPhysics(part.gameObject);
            ApplyExplosionForce(meshPartRigidBody);
        }
        StartCoroutine(HideExplodedParts(_meshPartsList));
    }
    private Rigidbody InitializePartsPhysics(GameObject meshPart)
    {
        Rigidbody partRigidbody = meshPart.AddComponent<Rigidbody>();
        partRigidbody.constraints = RigidbodyConstraints.FreezePositionZ
            | RigidbodyConstraints.FreezeRotationX
            | RigidbodyConstraints.FreezeRotationY;
        return partRigidbody;
    }
    private void ApplyExplosionForce(Rigidbody explodedPart)
    {
        explodedPart.AddExplosionForce(_explosionForce,
            transform.position,
            _explosionRadius,
            _explosionUpwardsModifier,
            ForceMode.VelocityChange);
    }
    private IEnumerator HideExplodedParts(List<Transform> explodedParts)
    {
        yield return new WaitForSeconds(_explodedPartsDestroyTime);
        foreach (Transform part in explodedParts)
        {
            part.gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
