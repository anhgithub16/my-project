export default function callApi(data){
    return new Promise((resolve,reject) => {
        const url = 'http://localhost:3001/items?q='+data.textSearch;
        fetch(url,{
            method:'GET'
        })
            .then((response)=>response.json())
            .then((res) => {
                resolve(res);
                console.log(res);
            })
            .catch((error)=>{
                reject(error);
            });
    });
}