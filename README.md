# Банк.
##  Функціональні вимоги:
###  Банк:
#### Типовий представник банкір:
- Повинен мати можливість вести моніторинг усіх клієнтів та банківських відділень (GET) та моніторинг конкретного відділення\клієнта (GET by ID);
###  Аккаунт клієнта:
#### Типовий представник клієнт:
- Повинен мати можливість вести моніторинг та моніторинг свого аккаунта (GET by ID);
-	Повинен мати можливість додавати нові данні про себе (POST);
-	Повинен мати можливість вносити корективи у свій баланс (PUT та PATCH);
-	Повинен мати можливість видаляти аккаунт (DELETE);
-	Повинен мати можливість моніторити баланс.
##  Методи
###  Банк:
####  GET:
Url: /api/[controller]
<br/>Вхідна модель: {}
<br/>Вихідна модель:
<br/>{
<br/>id : int, min=1, max=65535
<br/>Location: string, min=1, max=255
<br/>Head: string, min=1, max=255
<br/>Count of workers: int, min=0, max=65535
<br/>}
<br/>Метод передбачає реалізацію pagination у розмірі п'яти елементів за один запит
<br/>У разі виникненян помилки передавати Error (404)| BadRequest(404) | InternalServerError (500)
####  GET(id):
Url: /api/[controller]/{id}
<br/>Вхідна модель: {id : int, min=1, max=65535}
<br/>Вихідна модель:
<br/>{
<br/>id : int, min=1, max=65535
<br/>Location: string, min=1, max=255
<br/>Head: string, min=1, max=255
<br/>Count of workers: int, min=0, max=65535
<br/>}
<br/>У разі виникненян помилки передавати Error (404)| BadRequest(404) | InternalServerError (500)

###  Аккаунт клієнта
####  GET(id):
Url: /api/[controller]/{id}
<br/>Вхідна модель: {id : int, min=1, max=65535}
<br/>Вихідна модель:
<br/>{id : int, min=1, max=65535
<br/>name: string, min=1, max=255
<br/>second_name: string, min=1, max=255
<br/>sum: float, min=1, max=65535.0
<br/>}}
<br/>У разі виникненян помилки передавати Error (404)| BadRequest(404) | InternalServerError (500)
####  POST:
Url: /api/[controller]/
<br/>Вхідна модель: 
<br/>{id : int, min=1, max=65535
<br/>name: string, min=1, max=255
<br/>second_name: string, min=1, max=255
<br/>sum: float, min=1, max=65535.0
<br/>}
<br/>Вихідна модель:
<br/>{id : int, min=1, max=65535
<br/>name: string, min=1, max=255
<br/>second_name: string, min=1, max=255
<br/>sum: float, min=1, max=65535.0
<br/>}
<br/>У разі виникненян помилки передавати Error (404)| BadRequest(404) | InternalServerError (500)
####  Patch(id):
Url: /api/[controller]/{id}
<br/>Вхідна модель: 
<br/>{
<br/>id : int, min=1, max=65535
<br/>sum : float, min=1, max=65535.0 
<br/>}
<br/>Вихідна модель:
<br/>{
<br/>id: int min=1, max=65535
<br/>status: string, min=1, max=255
<br/>}
<br/>У разі виникненян помилки передавати Error (404)| BadRequest(404) | InternalServerError (500)
#### DELETE/[controller]/(id) 
Url: /api/v1/shops/{id}
<br/>Вхідна модель: 
<br/>{ id: int, min=1, max=65535}
<br/>Вихідна модель:
<br/>{ isDeleted: string, min=1, max=255}
<br/>У разі виникненян помилки передавати Error (404)| BadRequest(404) | InternalServerError (500)



##  Нефункціональні вимоги:
-	Безпека та конфіденційність;
-	Надійність;
-	Відновлювальність;
-	Продуктивність (час виконання запитів не більше двох секунд);
-	Збереження даних;
-	Керування помилками.
