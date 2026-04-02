using Extra;
using UnityEngine;
using UnityEngine.Pool;


namespace Manager
{
    public class PoolManager : Singleton<PoolManager>
    {
        [Header("Projectile")]
        [SerializeField] private Projectile.Projectile arrowPrefab;
        // ToDo 
        // Mehr Geschosse
        [Header("VFX")]
        // ToDo
        // Effect einfügen und über den PoolManager laufen lassen!
        

        public ObjectPool<Projectile.Projectile> ArrowPool { get; private set; }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        protected override void Awake()
        {
            base.Awake();
            ArrowPool = CreatePool(arrowPrefab);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////
        private static ObjectPool<Projectile.Projectile> CreatePool(Projectile.Projectile prefab)
        {
            ObjectPool<Projectile.Projectile> pool = null;
                pool = new ObjectPool<Projectile.Projectile>(
                createFunc: () =>
                {
                    var p = Instantiate(prefab);
                    p.SetPool(pool);
                    return p;
                },
                actionOnGet: p =>
                {
                    p.ResetProjectile();
                    p.gameObject.SetActive(true);
                },
                actionOnRelease: p => p.gameObject.SetActive(false),
                actionOnDestroy: p => Destroy(p.gameObject),
                collectionCheck: true,
                defaultCapacity: 30,
                maxSize: 100
            );
            return pool;
        }
    }
}