# 유니티 AR 프로젝트
<hr>

### 😎 주제 
- <b>증강현실 기반 로컬 소셜 네트워크</b>

### 🧱 서비스 구성요소
- UI
    1. 시작화면
        - 따로 회원가입 x, 인풋필드에 넣은 이름, 패스워드 사용
        ![시작화면](./myResources/startup.PNG)
    
    2. 메인화면
        - 가로 뷰에 후면 카메라 사용.
        - 기존 포스트 터치 -> 3
        - 새 포스트 터치 -> 4
        ![메인화면](./myResources/view.PNG)
   
    3. 기존 포스트 로딩
        - 작성자 이름, 작성 시간 표시
        - ~~수정버튼~~
        - 숨기기 버튼 : 해당 포스터를 표시안함.
        - 삭제 버튼 : 사용자 이름과 암호 비교 맞으면 삭제 가능
        ![기존 포스트](./myResources/oldPost.PNG)

    4. 새로운 포스트 작성
        - 포스트 : 콘텐츠 내용, 현재 위치에 포스트 데이터 생성 -> 2
        - 취소 : -> 2
        ![새로운 포스트](./myResources/newPost.PNG)

### ❓ Check How to
- Cloud Anchor

## ✔ 진행 사항(To do) 
- [ ] 디자인

### MainScene

### PlayScene
- [x] GPS 값 받아서 주변에 기존 포스트 표시

### OldPostScene
- [x] DB에서 내용 불러오기(작성자, 작성시간-간단히-, 내용)
- [x] 'Delete' if(curUser == authorUser) this.delete()
~~- [ ] 'Modifiy' if(curUser == authorUser) this.update(content, time)~~
~~- [ ] 'Hide' curUser.hide(this)~~

### NewPostScene

### Else
- [ ] Extras(ripple🐳 / Like👍)
- [ ] Cloud Anchor

### Today's To do!
- [x] 프리팹 에셋 적용
- [x] 프리팹에 작성자 이름, 작성일시 표시, 가능하면 내용일부도?
- [x] 카메라 위치 초기화 안됨
- [x] 상자가 카메라쪽 바라보게.
- [ ] 한글 및 영어 폰트
- [ ] 코드 정리
- [ ] 데이터 정해진 갯수안에서 랜덤하게 픽(ex 10개 다차면 그만찾아)