Proje Core, Data, Service ve API katmanından oluşmaktadır.Projede repository design pattern ve unit of work kullanılmıştır.

  ![image](https://github.com/ahmetKM/ProductApp-Study/assets/34060992/fd6af3ce-91a9-4ef0-bd44-06ff009e562c)

VeriTabanı olarak MSSQL kullanılmıştır.Bağlantı için localdb kullanılmıştır.
 
ORM olarak Entity Framework code first uygulanmıştır. Product, Category ve User tablosu oluşturulmuştur.

GenericRepository’de ortak işlemler tanımlanmıştır. Product ve Category repoları GenericRepository’den türemiştir. Ortak olmayan işlemler de bu class’lara eklenmiştir.

 ![image](https://github.com/ahmetKM/ProductApp-Study/assets/34060992/bdc0e012-b867-44e6-a1b0-ea258de3c583)

![image](https://github.com/ahmetKM/ProductApp-Study/assets/34060992/619f0fa0-cd21-436a-aadd-d1b92f65a7b6)

Kimlik Doğrulama JWT Token ile yapılmıştır. Giriş yapmak için “ahmet” ve “123456” bilgileri ile giriş yapılarak gelen token ile authorize işlemi swagger üzerinde yapılabilir.
 
![image](https://im2.ezgif.com/tmp/ezgif-2-c7e2c83255.gif)


Controller’larda ve servislerde kod sadeliği için mapping işleminde kullanmak üzere AutoMapper kullanılmıştır.
Kategoriler sürekli değişim göstermediği için kategoriler redisten çekilmiştir. Eğer redis boş ise entityframework ile veritabanından çekilmektedir. 
CategoryService class’ı Service class’ından kalıtım aldığı ve GetAllAsync metodu da generic olarak ayarlandığı için GetAllAsync metodu override edilmiştir.

 ![image](https://github.com/ahmetKM/ProductApp-Study/assets/34060992/e28e00ad-851a-4387-9954-b3e1bccb36d5)

 ![image](https://github.com/ahmetKM/ProductApp-Study/assets/34060992/77e87981-f609-4e42-9d6c-e773d222ca59)


Yeni bir kategori eklendiğinde tüm kullanıcılara mailin atılacağı bir senaryo düşünüldü. 
Bu işlem kullanıcıyı kategori ekledikten sonra uzun süre bekleteceği için bu işlem arka planda yapılmalıdır. 
Bunun için de bir mesaj kuyruk yapısına ihtiyaç olduğundan rabbitMq kullanıldı.
Eklenen kategori sonrası kullanıcı mailleri kuyruğa eklenmiştir.

![image](https://github.com/ahmetKM/ProductApp-Study/assets/34060992/878913d5-1014-4411-91e1-9f21a31900a4)

 
CategoryService class’ı Service class’ından kalıtım aldığı ve AddAsync metodu da generic olarak ayarlandığı için AddAsync metodu override edilmiştir.
 
 ![image](https://github.com/ahmetKM/ProductApp-Study/assets/34060992/f00b7467-268e-430d-a49b-70594688d438)


