import axios from 'axios';
//תפיסת שגיאות
axios.interceptors.request.use((config)=>
config,(error)=>
{console.log(error);
return Promise.reject(error);});

axios.interceptors.response.use((response)=>
response,(error)=>
{console.log(error);
  return Promise.reject(error);});
//ניתוב דפולטיבי
axios.defaults.baseURL = 'http://localhost:5034/Items/';
const apiUrl = "http://localhost:5034/"
//שליפה
export default {
  getTasks: async () => {
  const result = await axios.get(`${apiUrl}`)    
  return result.data;
  },
//הוספה
  addTask: async(name)=>{
    await axios.post(`${name}`) 
    return {}
  },
//עדכון
  setCompleted: async(id, IsComplete)=>{
    const result = await axios.put(`${id}/${IsComplete}`)
    return {};
  },
//מחיקה
  deleteTask:async(id)=>{
    const result = await axios.delete(`${id}`)
    return {};
  },

};
