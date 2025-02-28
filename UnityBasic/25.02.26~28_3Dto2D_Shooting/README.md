Including DownloadAssets

My Asset
1. FONT

Unity Asset
1. TMPro    :   UI-TextMeshPro

Asset Store
1. Cartoon FX Remaster Free     :   
https://assetstore.unity.com/packages/p/cartoon-fx-remaster-free-109565

2. Rockets, Missiles & Bombs - Cartoon Low Poly Pack    :   
https://assetstore.unity.com/packages/p/rockets-missiles-bombs-cartoon-low-poly-pack-73141

3. Awesome Cartoon Airplanes    :   
https://assetstore.unity.com/packages/3d/vehicles/air/awesome-cartoon-airplanes-56715

4. Space Star Field Backgrounds :
https://assetstore.unity.com/packages/2d/textures-materials/space-star-field-backgrounds-109689

5. Free Sci-Fi Music Collection :   
https://assetstore.unity.com/packages/audio/music/free-sci-fi-music-collection-287462

1. LevelManager
UI상에서 표시되는 레벨만을 관리

2. Bullet
총알 이동, 충돌체 파괴 관리
레이어 관리를 통한 오너레이어 그룹과의 충돌 무시

3. EmemyManager
Enemy스포너 역할
레벨 매니저를 통해 적 생성을 제어

4. Enemy
    4.1 EnemyMove
    적의 이동 제어, 플레이어와 충돌하면 둘다 파괴
    파괴 당할때 무조건 점수 증가시키기

    4.2 EnemyController
    적이 나를 바라보도록 제어
    레벨에 따른 총알 발사

5. ScoreManager
점수 시스템 관리
레벨, 생존 시간에 따른 점수 증가
적 파괴시 추가 증가

6. Player
플레이어 관리
    6.1 PlayerMove
    시작 시퀀스 존재
    PlayerText, TimeText제어
    플레이어가 맵밖으로 나가지 못하게 통제

    6.2 PlayerFire
    플레이어의 폭탄과 총탄을 제어

7. Camera
카메라는 플레이어를 따라다님
일정 범위 안에서는 자유롭게 플레이어가 움직일 수 있다

8. PhysicsManager
레이어 별 물리적 충돌 관리