using Extra;
using UnityEngine;
using UnityEngine.Pool;


namespace Manager
{
    public class PoolManager : Singleton<PoolManager>
    {
        [Header("Container")] 
        [SerializeField] private Transform projectileContainer;
        
        [Header("Projectile")]
        [SerializeField] private Projectile.Projectile arrowPrefab;
        [SerializeField] private Projectile.Projectile fireBallPrefab;
        // ToDo 
        // Mehr Geschosse
        [Header("VFX")]
        // ToDo
        // Effect einfügen und über den PoolManager laufen lassen!
        

        public ObjectPool<Projectile.Projectile> ArrowPool { get; private set; }
        public ObjectPool<Projectile.Projectile> FireBallPool { get; private set; }
        protected override bool PersistAcrossScenes => false;
        
        ////////////////////////////////////////////////////////////////////////////////////////////////
        protected override void Awake()
        {
            if (Instance != null) Destroy(Instance.gameObject);
            base.Awake();
            ArrowPool = CreatePool(arrowPrefab, projectileContainer);
            FireBallPool = CreatePool(fireBallPrefab, projectileContainer);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////
        private static ObjectPool<Projectile.Projectile> CreatePool(Projectile.Projectile prefab, Transform container)
        {
            ObjectPool<Projectile.Projectile> pool = null;
                pool = new ObjectPool<Projectile.Projectile>(
                createFunc: () =>
                {
                    var p = Instantiate(prefab, container);
                    p.SetPool(pool);
                    return p;
                },
                actionOnGet: p =>
                {
                    p.ResetProjectile();
                    p.gameObject.SetActive(true);
                },
                actionOnRelease: p => p.gameObject.SetActive(false),
                actionOnDestroy: p =>
                {
                    if(p != null) Destroy(p.gameObject);  
                },
                collectionCheck: true,
                defaultCapacity: 30,
                maxSize: 100
            );
            return pool;
        }
    }
}