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
- 데이터 베이스 사용방법

## ✔ 진행 사항(To do) 

### MainScene
- [x] 입력값(ID, PW) 저장할 변수 -> 스태틱으로 올려야함
- [x] Start 버튼을 눌렀을때 Play씬 로딩,
- [ ] 예쁜 디자인 - 배경, 버튼, 인풋필드

### PlayScene
- [ ] AR씬(파운데이션 적용)
- [ ] GPS 값 받아서 주변에 기존 포스트 표시
- [x] 기존 포스트 클릭하면 Old씬 로딩
- [x] 뉴 포스트 클릭하면 new씬 로딩
- [ ] 디자인 - 포스트 버튼

### OldPostScene
- [ ] DB에서 내용 불러오기(작성자, 작성시간-간단히-, 내용)
- [ ] 'Modifiy' if(curUser == authorUser) this.update(content, time)
- [ ] 'Hide' curUser.hide(this)
- [ ] 'Delete' if(curUser == authorUser) this.delete()
- [ ] 디자인 - 버튼, 컨텐츠 창

### NewPostScene
- [ ] 'Post' - 컨텐츠 작성해 DB로 올리기(작성자, 작성시간, 내용)
- [ ] 'Cancel' load(PlayScene)
- [ ] 디자인 - 버튼 


### Else
- [ ] Database
- [ ] ?